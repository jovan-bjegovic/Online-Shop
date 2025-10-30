using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Core.UseCases.Categories.DeleteExpired;

namespace OnlineShop.CategoryCleanup.Workers;

public class CategoryCleanupWorker : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    private readonly TimeSpan workerInterval;
    private readonly TimeSpan deletionThreshold;

    public CategoryCleanupWorker(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        this.serviceProvider = serviceProvider;

        var section = configuration.GetSection("CategoryCleanup");
        workerInterval = TimeSpan.FromSeconds(section.GetValue<int>("WorkerInterval"));
        deletionThreshold = TimeSpan.FromSeconds(section.GetValue<int>("DeletionThreshold"));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();

            var useCase = scope.ServiceProvider.GetRequiredService<DeleteExpiredCategoriesUseCase>();

            DeleteExpiredCategoriesRequest request = new DeleteExpiredCategoriesRequest
            {
                CutoffDate = DateTime.UtcNow - deletionThreshold
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

            await Task.Delay(workerInterval, stoppingToken);
        }
    }
}