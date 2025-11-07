using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.SetProductImage;

public class SetProductImageUseCase(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : IUseCase<SetProductImageRequest, SetProductImageResponse>
{
    public async Task<SetProductImageResponse> Execute(SetProductImageRequest request)
    {
        Product? product = await productRepository.FindByIdAsync(request.Id);
        if (product == null)
            throw new ArgumentException($"Product '{request.Id}' not found.");

        product.ImageId = request.ImageId;
        await productRepository.UpdateProductAsync(product);

        await unitOfWork.CommitAsync();

        return new SetProductImageResponse
        {
            Product = product
        };
    }
}