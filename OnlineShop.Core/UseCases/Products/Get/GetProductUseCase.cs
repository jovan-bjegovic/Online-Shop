using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.Get;

public class GetProductUseCase(
    IProductRepository repository)
    : IUseCase<GetProductRequest, GetProductResponse>
{
    public async Task<GetProductResponse> Execute(GetProductRequest request)
    {
        if (request.Id == Guid.Empty)
        {
            throw new ArgumentException("Product ID must be provided.");
        }
        
        Product? product = await repository.FindByIdAsync(request.Id);

        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID '{request.Id}' not found.");
        }

        return new GetProductResponse
        {
            Product = product
        };
    }
}
