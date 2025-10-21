using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Core.Interfaces;
using OnlineShop.Data.Repositories;

namespace OnlineShop.Data;

public static class DIConfiguration
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration,
        string? jsonFilePath = null)
    {
        var dbHost = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
        var dbPort = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
        var dbName = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "mydb";
        var dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
        var dbPass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "postgres";

        var connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPass}";

        if (!string.IsNullOrEmpty(connectionString))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<ICategoryRepository, DbCategoryRepository>();
        }
        else if (!string.IsNullOrEmpty(jsonFilePath))
        {
            services.AddSingleton<ICategoryRepository>(new JsonCategoryRepository(jsonFilePath));
        }

        return services;
    }
}