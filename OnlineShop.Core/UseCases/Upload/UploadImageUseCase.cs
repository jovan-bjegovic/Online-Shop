using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Upload;

public class UploadImageUseCase(
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork)
    : IUseCase<UploadImageRequest, UploadImageResponse>
{
    public async Task<UploadImageResponse> Execute(UploadImageRequest request)
    {
        if (request.Image == null || request.Image.Length == 0)
        {
            throw new ArgumentException("File is empty.");
        }
        
        if (!request.Image.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Invalid file type. Only images are allowed.");
        }
        
        string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "images");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }
        
        string originalFileName = Path.GetFileNameWithoutExtension(request.Image.FileName);
        string extension = Path.GetExtension(request.Image.FileName);
        string fileName = originalFileName + extension;
        string filePath = Path.Combine(uploadsFolder, fileName);

        if (File.Exists(filePath))
        {
            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            fileName = $"{originalFileName}_{timestamp}{extension}";
            filePath = Path.Combine(uploadsFolder, fileName);
        }

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.Image.CopyToAsync(stream);
        }

        DateTime createdAt = DateTime.UtcNow;

        var image = new Image
        {
            FileName = fileName,
            FilePath = $"/uploads/images/{fileName}",
            Size = request.Image.Length,
            CreatedAt = createdAt
        };
        
        await imageRepository.AddAsync(image);
        await unitOfWork.CommitAsync();

        return new UploadImageResponse
        {
            Id = image.Id,
            FileName = image.FileName,
            FilePath = image.FilePath,
            Size = image.Size,
            CreatedAt = image.CreatedAt
        };
    }
}