using Microsoft.AspNetCore.Http;

namespace OnlineShop.Core.UseCases.Upload;

public class UploadFileRequest
{
    public IFormFile File { get; set; } = default!;
}