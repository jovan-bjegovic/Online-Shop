using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Image = SixLabors.ImageSharp.Image;
using ModelImage = OnlineShop.Core.Models.Image;

namespace OnlineShop.Core.UseCases.Products.SetProductImage;

public class SetProductImageUseCase(
    IProductRepository productRepository,
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork)
    : IUseCase<SetProductImageRequest, SetProductImageResponse>
{
    public async Task<SetProductImageResponse> Execute(SetProductImageRequest request)
    {
        if (request.ImageId == Guid.Empty)
        {
            throw new ArgumentException("ImageId cannot be null or empty.");
        }
        
        Product? product = await productRepository.FindByIdAsync(request.Id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product '{request.Id}' not found.");
        }
        
        ModelImage? image = await imageRepository.FindByIdAsync(request.ImageId);
        
        if (image == null)
        {
            throw new KeyNotFoundException($"Image '{request.ImageId}' not found.");
        }        
        
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), image.FilePath.TrimStart('/'));

        if (!File.Exists(imagePath))
            throw new FileNotFoundException($"Image file not found on disk: {imagePath}");

        using (Image<Rgba32> loadedImage = await Image.LoadAsync<Rgba32>(imagePath))
        {
            const int minWidth = 600;
            const int minHeight = 400;
            const double minRatio = 4.0 / 3.0;
            const double maxRatio = 16.0 / 9.0;

            if (loadedImage.Width < minWidth || loadedImage.Height < minHeight)
            {
                throw new ArgumentException($"Image must be at least {minWidth}x{minHeight} pixels.");
            }

            double ratio = (double)loadedImage.Width / loadedImage.Height;
            if (ratio < minRatio || ratio > maxRatio)
            {
                throw new ArgumentException("Image aspect ratio must be between 4:3 and 16:9.");
            }
        }

        product.ImageId = request.ImageId;

        await productRepository.UpdateProductAsync(product);
        await unitOfWork.CommitAsync();

        return new SetProductImageResponse
        {
            Product = product
        };
    }
}
