using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.Enable;

public class EnableProductsUseCase(
    IProductRepository repository,
    IUnitOfWork unitOfWork
) : IUseCase<EnableProductsRequest, EnableProductsResponse>
{
    public async Task<EnableProductsResponse> Execute(EnableProductsRequest request)
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
            product.Enabled = true;
        }

        await repository.UpdateProductsAsync(products);
        await unitOfWork.CommitAsync();

        return new EnableProductsResponse
        {
            Products = products.Select(p => new ProductStatusItem
            {
                Id = p.Id,
                Title = p.Title,
                Enabled = p.Enabled
            }).ToList()
        };
    }
}