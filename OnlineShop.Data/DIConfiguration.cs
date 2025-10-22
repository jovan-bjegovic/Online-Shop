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
        IConfiguration configuration)
    {

        string dbHost = GetEnv("POSTGRES_HOST");
        string dbPort = GetEnv("POSTGRES_PORT");
        string dbName = GetEnv("POSTGRES_DB");
        string dbUser = GetEnv("POSTGRES_USER");
        string dbPass = GetEnv("POSTGRES_PASSWORD");

        string connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPass}";
        if (!string.IsNullOrEmpty(connectionString))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<ICategoryRepository, DbCategoryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        return services;
    }
    
    private static string GetEnv(string name)
    {
        string? value = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Environment variable '{name}' is not set.");
        }

        return value;
    }
}