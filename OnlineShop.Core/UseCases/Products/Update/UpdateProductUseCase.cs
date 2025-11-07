using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.Update;

public class UpdateProductUseCase(
    IProductRepository repository,
    IUnitOfWork unitOfWork
) : IUseCase<UpdateProductRequest, UpdateProductResponse>
{
    public async Task<UpdateProductResponse> Execute(UpdateProductRequest request)
    {
        Product? product = await repository.FindByIdAsync(request.Id);

        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID '{request.Id}' not found.");
        }
            
        if (!string.IsNullOrWhiteSpace(request.Sku))
        {
            bool skuExists = await repository.SkuExistsAsync(request.Sku, request.Id);
            if (skuExists)
            {
                throw new InvalidOperationException($"SKU '{request.Sku}' already exists.");
            }

            product.Id = request.Id;
        }
            
        bool categoryExists = await repository.CategoryExistsAsync(request.CategoryId);
        if (!categoryExists)
        {
            throw new KeyNotFoundException($"Category '{request.CategoryId}' not found.");
        }

        product.Title = request.Title;
        product.Brand = request.Brand;
        product.Sku = request.Sku;
        product.CategoryId = request.CategoryId;
        product.ShortDescription = request.ShortDescription;
        product.LongDescription = request.LongDescription;
        product.Price = request.Price;
        product.Image = request.Image;
        product.Enabled = request.Enabled;

        await repository.UpdateProductAsync(product);
        await unitOfWork.CommitAsync();

        return new UpdateProductResponse
        {
            Id = product.Id,
            Title = product.Title,
            Brand = product.Brand,
            Sku = product.Sku,
            CategoryId = product.CategoryId,
            ShortDescription = product.ShortDescription,
            LongDescription = product.LongDescription,
            Price = product.Price,
            Image = product.Image,
            Enabled = product.Enabled
        };
    }
}