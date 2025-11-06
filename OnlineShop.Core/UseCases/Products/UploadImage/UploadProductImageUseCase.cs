using OnlineShop.Core.Interfaces;

namespace OnlineShop.Core.UseCases.Products.UploadImage;

public class UploadProductImageUseCase : IUseCase<UploadProductImageRequest, UploadProductImageResponse>
{
    private readonly string uploadsFolder;

    public UploadProductImageUseCase()
    {
        uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "images");
        Directory.CreateDirectory(uploadsFolder);
    }

    public async Task<UploadProductImageResponse> Execute(UploadProductImageRequest request)
    {
        if (request.File == null || request.File.Length == 0)
        {
            throw new ArgumentException("File is empty.");
        }
        if (!request.File.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Invalid file type. Only images are allowed.");
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