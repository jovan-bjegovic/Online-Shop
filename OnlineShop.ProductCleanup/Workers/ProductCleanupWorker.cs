using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.UseCases.Products.DeleteExpired;
using OnlineShop.ProductCleanup.Options;

namespace OnlineShop.ProductCleanup.Workers;

public class ProductCleanupWorker : BackgroundService
{
    private readonly IServiceProvider serviceProvider;
    private readonly TimeSpan workerInterval;
    private readonly TimeSpan deletionThreshold;

    public ProductCleanupWorker(
        IServiceProvider serviceProvider,
        IOptions<ProductCleanupOptions> options)
    {
        this.serviceProvider = serviceProvider;

        ProductCleanupOptions opt = options.Value;
        workerInterval = TimeSpan.FromSeconds(opt.WorkerInterval);
        deletionThreshold = TimeSpan.FromSeconds(opt.DeletionThreshold);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = serviceProvider.CreateScope();

            var useCase = scope.ServiceProvider
                .GetRequiredService<IUseCase<DeleteExpiredProductsRequest, DeleteExpiredProductsResponse>>();

            var request = new DeleteExpiredProductsRequest
            {
                CutoffDate = DateTime.UtcNow - deletionThreshold
            };

            DeleteExpiredProductsResponse response = await useCase.Execute(request);

            if (response.Count > 0)
            {
                Console.WriteLine($"[{DateTime.UtcNow}] Permanently deleted {response.Count} products:");
                foreach (var p in response.DeletedProducts)
                {
                    Console.WriteLine($" - {p.Title} ({p.Sku}) deleted at {p.DeletedAt}");
                }
            }

            await Task.Delay(workerInterval, stoppingToken);
        }
    }
}