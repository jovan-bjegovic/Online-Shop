using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnlineShop.Core.Interfaces;
using OnlineShop.Data.Options;
using OnlineShop.Data.Repositories;

namespace OnlineShop.Data;

public static class DIConfiguration
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        services.Configure<DatabaseOptions>(configuration.GetSection("ConnectionStrings"));

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.UseNpgsql(dbOptions.DefaultConnection);
        });

        services.AddScoped<ICategoryRepository, DbCategoryRepository>();
        services.AddScoped<IUserRepository, DbUserRepository>();
        services.AddScoped<IProductRepository, DbProductRepository>();
        services.AddScoped<IImageRepository, DbImageRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

}