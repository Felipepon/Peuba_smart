using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TravelAgency.Infrastructure.Data;
using FluentValidation;
using MediatR;
using TravelAgency.Domain.Interfaces;
using TravelAgency.Infrastructure.Repositories;
using TravelAgency.Application.Features.Hotels.Commands;
using TravelAgency.Application.Features.Hotels.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Globalization;




var builder = WebApplication.CreateBuilder(args);


builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Configurar DbContext para MySQL
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MariaDbServerVersion(new Version(10, 4, 32)),
        mysqlOptions => mysqlOptions.EnableRetryOnFailure()
    )
);

var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key no configurada.");
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

// Validar longitud mínima de la clave
if (keyBytes.Length < 32) 
    throw new Exception("La clave debe tener al menos 256 bits (32 bytes).");

var securityKey = new SymmetricSecurityKey(keyBytes);
var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

// Configurar autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
       options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = securityKey,
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero,
    RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" // 👈 Configura el rol esperado
};


    });

// Configurar autorización
builder.Services.AddAuthorization(options => 
{
    options.AddPolicy("Agent", policy => 
        policy.RequireClaim(ClaimTypes.Role, "Agent")); // 👈 Claim específico
});

// Registrar dependencias
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreateHotelCommand).Assembly));

builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateHotelCommandValidator>();

// Configurar Swagger con seguridad JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Travel Agency API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment()) 
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Endpoint para generar token de prueba
app.MapPost("/api/auth/login", () => 
{
   var claims = new List<Claim>
{
    new Claim(JwtRegisteredClaimNames.Sub, "1234567890"),
    new Claim(JwtRegisteredClaimNames.Name, "John Doe"),
    new Claim(ClaimTypes.Role, "Agent"), // 👈 Esto sigue igual
    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
};


    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials
    );

    return new { Token = new JwtSecurityTokenHandler().WriteToken(token) };
});

Console.WriteLine($"JWT Key en API: {builder.Configuration["Jwt:Key"]}");




app.Run();
