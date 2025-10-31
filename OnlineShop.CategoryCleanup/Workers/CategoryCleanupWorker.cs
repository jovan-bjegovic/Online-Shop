using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OnlineShop.CategoryCleanup.Options;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.UseCases.Categories.DeleteExpired;

namespace OnlineShop.CategoryCleanup.Workers;

public class CategoryCleanupWorker : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    private readonly TimeSpan workerInterval;
    private readonly TimeSpan deletionThreshold;

    public CategoryCleanupWorker(
        IServiceProvider serviceProvider,
        IOptions<CategoryCleanupOptions> options)
    {
        this.serviceProvider = serviceProvider;

        var opt = options.Value;
        workerInterval = TimeSpan.FromSeconds(opt.WorkerInterval);
        deletionThreshold = TimeSpan.FromSeconds(opt.DeletionThreshold);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();

            IUseCase<DeleteExpiredCategoriesRequest, DeleteExpiredCategoriesResponse> useCase = scope.ServiceProvider.GetRequiredService<DeleteExpiredCategoriesUseCase>();

            DeleteExpiredCategoriesRequest request = new DeleteExpiredCategoriesRequest
            {
                CutoffDate = DateTime.UtcNow - deletionThreshold
            };

            DeleteExpiredCategoriesResponse response = await useCase.Execute(request);

            if (response.Count > 0)
            {
                Console.WriteLine($"[{DateTime.UtcNow}] Permanently deleted {response.Count} categories:");
                foreach (DeletedCategoryInfo c in response.DeletedCategories)
                {
                    Console.WriteLine($" - {c.Title} ({c.Code}) deleted at {c.DeletedAt}");
                }
            }

            await Task.Delay(workerInterval, stoppingToken);
        }
    }
}