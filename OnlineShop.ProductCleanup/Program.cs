using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.ProductCleanup.Options;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.UseCases.Products.DeleteExpired;
using OnlineShop.Data;
using OnlineShop.Data.Repositories;
using OnlineShop.ProductCleanup.Workers;

var hostBuilder = Host.CreateDefaultBuilder(args)
    .UseEnvironment(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development")
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.Configure<ProductCleanupOptions>(
            configuration.GetSection("ProductCleanup"));

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddScoped<IProductRepository, DbProductRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUseCase<DeleteExpiredProductsRequest, DeleteExpiredProductsResponse>, DeleteExpiredProductsUseCase>();
        services.AddHostedService<ProductCleanupWorker>();
    })
    .Build();

await hostBuilder.RunAsync();