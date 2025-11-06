using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Products.SetProductImage;

namespace OnlineShop.Core.UseCases.Products.Get;

public class GetProductUseCase(
    IProductRepository repository)
    : IUseCase<GetProductRequest, GetProductResponse>
{
    public async Task<GetProductResponse> Execute(GetProductRequest request)
    {
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
