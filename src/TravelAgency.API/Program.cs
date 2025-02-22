using Microsoft.EntityFrameworkCore;
using TravelAgency.Infrastructure.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // Añadir este using

var builder = WebApplication.CreateBuilder(args);

// Registrar el DbContext para MySQL
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
         new MariaDbServerVersion(new Version(10, 4, 32)), // Versión de tu MySQL
        mysqlOptions => 
        {
            mysqlOptions.EnableRetryOnFailure();
        }
    )
);

// Servicios existentes
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuración del pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "¡La API de TravelAgency está funcionando con MySQL!");

app.Run();