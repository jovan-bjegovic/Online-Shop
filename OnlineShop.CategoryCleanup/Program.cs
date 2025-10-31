using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineShop.CategoryCleanup.Options;
using OnlineShop.CategoryCleanup.Workers;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.UseCases.Categories.DeleteExpired;
using OnlineShop.Data;
using OnlineShop.Data.Repositories;

var hostBuilder = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.Configure<CategoryCleanupOptions>(
            configuration.GetSection("CategoryCleanup"));
    
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<ICategoryRepository, DbCategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUseCase<DeleteExpiredCategoriesRequest, DeleteExpiredCategoriesResponse>, DeleteExpiredCategoriesUseCase>();
        services.AddHostedService<CategoryCleanupWorker>();
    })
    .Build();

await hostBuilder.RunAsync();