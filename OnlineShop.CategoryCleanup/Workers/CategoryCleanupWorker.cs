using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Core.Interfaces;

namespace OnlineShop.CategoryCleanup.Workers;

public class CategoryCleanupWorker(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly TimeSpan interval = TimeSpan.FromDays(1);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<ICategoryRepository>();

            int deletedCount = await repository.RemoveExpiredAsync(DateTime.UtcNow.AddDays(-30));
            
            if (deletedCount > 0)
            {
                Console.WriteLine($"Deleted {deletedCount} expired categories at {DateTime.UtcNow}");
            }

            await Task.Delay(interval, stoppingToken);
        }
    }
}