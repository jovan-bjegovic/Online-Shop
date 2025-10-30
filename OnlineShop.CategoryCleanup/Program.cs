using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using OnlineShop.CategoryCleanup.Workers;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.UseCases.Categories.DeleteExpired;
using OnlineShop.Data;
using OnlineShop.Data.Repositories;

if (args.Length < 5)
{
    Console.WriteLine("Usage: dotnet OnlineShop.CategoryCleanup.dll <host> <port> <db> <user> <password>");
    
    return;
}

string host = args[0];
string port = args[1];
string db = args[2];
string user = args[3];
string password = args[4];

string connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={password}";

var hostBuilder = Host.CreateDefaultBuilder()
    .ConfigureServices((_, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<ICategoryRepository, DbCategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<DeleteExpiredCategoriesUseCase>();
        services.AddHostedService<CategoryCleanupWorker>();
    })
    .Build();

await hostBuilder.RunAsync();