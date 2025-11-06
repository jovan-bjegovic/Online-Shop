using SixLabors.ImageSharp;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.UploadImage;

public class UploadProductImageUseCase
    : IUseCase<UploadProductImageRequest, UploadProductImageResponse>
{
    private readonly string uploadsFolder;
    private readonly IProductRepository repository;

    public UploadProductImageUseCase(IProductRepository repository)
    {
        this.repository = repository;
        uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "images");
        Directory.CreateDirectory(uploadsFolder);
    }

    public async Task<UploadProductImageResponse> Execute(UploadProductImageRequest request)
    {
        Product? product = await repository.FindBySkuAsync(request.Sku);
        if (product == null)
            throw new ArgumentException($"Product with SKU '{request.Sku}' not found.");

        if (request.File == null || request.File.Length == 0)
        {
            throw new ArgumentException("File is empty.");
        }        if (!request.File.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Invalid file type. Only images are allowed.");
        }
        using var image = await Image.LoadAsync(request.File.OpenReadStream());

        int width = image.Width;
        int height = image.Height;

        if (width < 600)
        {
            throw new ArgumentException("Image width must be at least 600px.");
        }
        double ratio = (double)width / height;
        if (ratio < 4.0 / 3 || ratio > 16.0 / 9)
        {
            throw new ArgumentException("Image aspect ratio must be between 4:3 and 16:9.");
        }
        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
        string filePath = Path.Combine(uploadsFolder, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream);
        }
        
        return new UploadProductImageResponse
        {
            Id = Guid.NewGuid(),
            FileName = fileName,
            FilePath = filePath,
            Size = request.File.Length,
            CreatedAt = DateTime.UtcNow
        };
    }
}
