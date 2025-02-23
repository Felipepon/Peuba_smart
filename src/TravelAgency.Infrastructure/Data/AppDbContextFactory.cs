// TravelAgency.Infrastructure/Data/AppDbContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TravelAgency.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = "Server=127.0.0.1;Port=3306;Database=travelagencydb;User=root;Password=12345;"; // Reemplaza con tu cadena

        optionsBuilder.UseMySql(
            connectionString,
            new MariaDbServerVersion(new Version(10, 4, 32)), // Versión de tu MySQL/MariaDB
            mysqlOptions => mysqlOptions.EnableRetryOnFailure()
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}