using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.Toggle;

public class ToggleProductsUseCase(
    IProductRepository repository,
    IUnitOfWork unitOfWork
) :  IUseCase<ToggleProductsRequest, ToggleProductsResponse>
{
    public async Task<ToggleProductsResponse> Execute(ToggleProductsRequest request)
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
            product.Enabled = !product.Enabled;
        }

        await repository.UpdateProductsAsync(products);
        await unitOfWork.CommitAsync();

        return new ToggleProductsResponse
        {
            Products = products.Select(p => new ToggleProductItem
            {
                Id = p.Id,
                Enabled = p.Enabled
            }).ToList()
        };
    }
}