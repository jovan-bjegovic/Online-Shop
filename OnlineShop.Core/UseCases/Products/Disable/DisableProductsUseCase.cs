using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.Disable;

public class DisableProductsUseCase(
    IProductRepository repository,
    IUnitOfWork unitOfWork
) : IUseCase<DisableProductsRequest, DisableProductsResponse>
{
    public async Task<DisableProductsResponse> Execute(DisableProductsRequest request)
    {
        if (request.Ids == null || request.Ids.Count == 0)
        {
            throw new ArgumentException("No product IDs provided.");
        }
        
        List<Product> products = await repository.FindByIdsAsync(request.Ids);

        if (products == null || products.Count == 0)
        {
            throw new KeyNotFoundException("No products found with the provided IDs.");
        }
        
        foreach (var product in products)
        {
            product.Enabled = false;
        }

        await repository.UpdateProductsAsync(products);
        await unitOfWork.CommitAsync();

        return new DisableProductsResponse
        {
            Products = products.Select(p => new ProductStatusItem
            {
                Id = p.Id,
                Enabled = p.Enabled
            }).ToList()
        };
    }
}