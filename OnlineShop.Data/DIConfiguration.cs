using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        var dbOptions = new DatabaseOptions();
        configuration.GetSection("ConnectionStrings").Bind(dbOptions);
        services.AddSingleton(dbOptions);

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var opts = serviceProvider.GetRequiredService<DatabaseOptions>();
            options.UseNpgsql(opts.DefaultConnection);
        });

        services.AddScoped<ICategoryRepository, DbCategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

}