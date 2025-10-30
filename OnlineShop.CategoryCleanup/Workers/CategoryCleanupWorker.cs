using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Core.UseCases.Categories.DeleteExpired;

namespace OnlineShop.CategoryCleanup.Workers;

public class CategoryCleanupWorker(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly TimeSpan interval = TimeSpan.FromMinutes(1);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();

            var useCase = scope.ServiceProvider.GetRequiredService<DeleteExpiredCategoriesUseCase>();

            DeleteExpiredCategoriesRequest request = new DeleteExpiredCategoriesRequest
            {
                CutoffDate = DateTime.UtcNow.AddMinutes(-2)
            };

            var response = await useCase.Execute(request);

            if (response.Count > 0)
            {
                Console.WriteLine($"[{DateTime.UtcNow}] Permanently deleted {response.Count} categories:");
                foreach (var c in response.DeletedCategories)
                {
                    Console.WriteLine($" - {c.Title} ({c.Code}) deleted at {c.DeletedAt}");
                }
            }

            await Task.Delay(interval, stoppingToken);
        }
    }
}