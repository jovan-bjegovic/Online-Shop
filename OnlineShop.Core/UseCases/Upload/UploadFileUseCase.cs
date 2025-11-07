using OnlineShop.Core.Interfaces;

namespace OnlineShop.Core.UseCases.Upload;

public class UploadFileUseCase : IUseCase<UploadFileRequest, UploadFileResponse>
{
    private readonly string uploadsFolder;

    public UploadFileUseCase()
    {
        uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "files");
        Directory.CreateDirectory(uploadsFolder);
    }

    public async Task<UploadFileResponse> Execute(UploadFileRequest request)
    {
        if (request.File == null || request.File.Length == 0)
        {
            throw new ArgumentException("File is empty.");
        }

        string fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
        string filePath = Path.Combine(uploadsFolder, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream);
        }

        return new UploadFileResponse
        {
            Id = Guid.NewGuid(),
            FileName = fileName,
            FilePath = filePath,
            Size = request.File.Length,
            CreatedAt = DateTime.UtcNow
        };
    }
}