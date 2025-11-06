using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Products.SetProductImage;

namespace OnlineShop.Core.UseCases.Products.Create;

public class CreateProductUseCase(
    IProductRepository repository,
    IUnitOfWork unitOfWork
) : IUseCase<CreateProductRequest, CreateProductResponse>
{
    public async Task<CreateProductResponse> Execute(CreateProductRequest request)
    {
        if (await repository.SkuExistsAsync(request.Sku))
        {
            throw new ArgumentException($"A product with SKU '{request.Sku}' already exists.");
        }

        if (!await repository.CategoryExistsAsync(request.CategoryId))
        {
            throw new KeyNotFoundException($"Category with ID '{request.CategoryId}' not found.");
        }

        Product product = new Product
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Sku = request.Sku,
            Brand = request.Brand,
            CategoryId = request.CategoryId,
            ShortDescription = request.ShortDescription,
            LongDescription = request.LongDescription,
            Price = request.Price,
            Image = request.Image,
            Enabled = true
        };

        await repository.CreateProductAsync(product);
        await unitOfWork.CommitAsync();

        return new CreateProductResponse
        {
            Id = product.Id,
            Title = product.Title,
            Sku = product.Sku,
            Brand = product.Brand,
            CategoryId = product.CategoryId,
            ShortDescription = product.ShortDescription,
            LongDescription = product.LongDescription,
            Price = product.Price,
            Image = product.Image,
            Enabled = product.Enabled
        };
    }
}