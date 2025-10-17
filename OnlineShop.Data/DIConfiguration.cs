using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Core.Interfaces;
using OnlineShop.Data.Repositories;

namespace OnlineShop.Data;

public static class DIConfiguration
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, string jsonFilePath)
    {
        services.AddSingleton<ICategoryRepository>(new JsonCategoryRepository(jsonFilePath));

        return services;
    }
}