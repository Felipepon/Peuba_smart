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
using System.Reflection;
using Microsoft.OpenApi.Any;
using TravelAgency.Application.Features.Bookings.Commands;
using TravelAgency.Application.Features.Rooms.Commands;
using TravelAgency.Application.Features.Rooms.Queries;
using TravelAgency.Application.Features.Bookings.Queries;
using TravelAgency.Application.DTOs;
using SendGrid;
using Swashbuckle.AspNetCore.SwaggerGen;
using TravelAgency.Infrastructure.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configurar SendGrid
builder.Services.AddSingleton<ISendGridClient>(new SendGridClient(builder.Configuration["SendGrid:ApiKey"]));
builder.Services.AddScoped<IEmailService, EmailService>();

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

// En TravelAgency.API/Program.cs
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreateHotelCommand).Assembly)); // Usa cualquier comando de Application

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
           RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" 
       };
    });

// Configurar autorización
builder.Services.AddAuthorization(options => 
{
    options.AddPolicy("Agent", policy => policy.RequireRole("Agent"));
    options.AddPolicy("Traveler", policy => policy.RequireRole("Traveler"));
});

// Registrar dependencias
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreateHotelCommand).Assembly));

builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateHotelCommandValidator>();

// Configurar Swagger con seguridad JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Travel Agency API", 
        Version = "v1",
        Description = "API para gestión de hoteles, habitaciones y reservas",
        Contact = new OpenApiContact
        {
            Name = "Equipo de Desarrollo",
            Email = "soporte@travelagency.com"
        }
    });

    // Configuración de seguridad JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: \"Bearer {token}\""
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

    // Ejemplos para Habitaciones (Rooms)
    c.MapType<CreateRoomCommand>(() => new OpenApiSchema
    {
        Example = (IOpenApiAny)new OpenApiObject
        {
            ["Type"] = new OpenApiString("Standard"),  
            ["BaseCost"] = new OpenApiDouble(100.0),
            ["Taxes"] = new OpenApiDouble(10.0),
            ["Location"] = new OpenApiString("Piso 3, Habitación 301")
        }
    });

    c.MapType<UpdateRoomCommand>(() => new OpenApiSchema
    {
        Example = (IOpenApiAny)new OpenApiObject
        {
            ["Taxes"] = new OpenApiDouble(15.0),
            ["Location"] = new OpenApiString("Piso 3, Habitación 301 (Actualizada)")
        }
    });

    // Personalizar documentación de endpoints de Habitaciones
    c.OperationFilter<RoomOperationFilters>();

    // Ejemplos para Hoteles
    // c.ExampleFilters();

    // Ejemplo para CreateHotelCommand
    c.MapType<CreateHotelCommand>(() => new OpenApiSchema
    {
        Example = (IOpenApiAny)new OpenApiObject
        {
            ["Name"] = new OpenApiString("Grand Hotel Paris"),
            ["City"] = new OpenApiString("Paris")
        }
    });

    // Ejemplo para Reservas
    c.MapType<CreateBookingCommand>(() => new OpenApiSchema
    {
        Example = (IOpenApiAny)new OpenApiObject
        {
            ["RoomId"] = new OpenApiString("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            ["CheckInDate"] = new OpenApiString("2024-05-01"),
            ["CheckOutDate"] = new OpenApiString("2024-05-05"),
            ["Guests"] = new OpenApiArray
            {
                new OpenApiObject
                {
                    ["FullName"] = new OpenApiString("Juan Pérez"),
                    ["BirthDate"] = new OpenApiString("1990-01-01"),
                    ["Gender"] = new OpenApiString("Male"),
                    ["DocumentType"] = new OpenApiString("Passport"),
                    ["DocumentNumber"] = new OpenApiString("AB123456"),
                    ["Email"] = new OpenApiString("juan@example.com"),
                    ["Phone"] = new OpenApiString("+1234567890")
                }
            },
            ["EmergencyContact"] = new OpenApiObject
            {
                ["FullName"] = new OpenApiString("María López"),
                ["Phone"] = new OpenApiString("+0987654321")
            }
        }
    });

    // Habilitar anotaciones XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

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
app.MapPost("/api/auth/login", (string role) => 
{
    var validRoles = new[] { "Agent", "Traveler" };
    if (!validRoles.Contains(role))
        return Results.BadRequest("Rol inválido. Opciones: Agent, Traveler");

    var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Name, "John Doe"),
        new Claim(ClaimTypes.Role, role), 
        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
    };

    var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: credentials
    );

    return Results.Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
});

Console.WriteLine($"JWT Key en API: {builder.Configuration["Jwt:Key"]}");

app.Run();

// Filtro para documentación de Habitaciones
public class RoomOperationFilters : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Documentación para POST /api/hotels/{hotelId}/rooms
        if (context.MethodInfo.Name == "CreateRoom")
        {
            operation.Summary = "Crear habitación";
            operation.Description = "Permite a los agentes agregar habitaciones a un hotel";
            operation.Responses.Add("201", new OpenApiResponse { Description = "Habitación creada exitosamente" });
            operation.Parameters[0].Description = "ID del hotel al que pertenece la habitación";
        }

        // Documentación para PUT /api/rooms/{id}
        if (context.MethodInfo.Name == "UpdateRoom")
        {
            operation.Summary = "Actualizar habitación";
            operation.Description = "Modifica los impuestos o ubicación de una habitación";
            operation.Parameters[0].Description = "ID de la habitación";
        }

        // Documentación para PATCH /api/rooms/{id}/toggle-status
        if (context.MethodInfo.Name == "ToggleRoomStatus")
        {
            operation.Summary = "Habilitar/Deshabilitar habitación";
            operation.Description = "Cambia el estado de disponibilidad de una habitación";
            operation.Parameters[0].Description = "ID de la habitación";
        }

        // Documentación para GET /api/hotels/{hotelId}/rooms
        if (context.MethodInfo.Name == "GetRoomsByHotel")
        {
            operation.Summary = "Listar habitaciones de un hotel";
            operation.Description = "Obtiene todas las habitaciones asociadas a un hotel";
            operation.Parameters[0].Description = "ID del hotel";
        }
    }
}