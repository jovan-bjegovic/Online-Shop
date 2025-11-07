using Microsoft.AspNetCore.Http;

namespace OnlineShop.Core.UseCases.Upload;

public class UploadImageRequest
{
    public IFormFile? Image { get; init; }
}