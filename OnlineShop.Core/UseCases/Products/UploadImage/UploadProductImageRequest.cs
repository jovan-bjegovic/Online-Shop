using Microsoft.AspNetCore.Http;

namespace OnlineShop.Core.UseCases.Products.UploadImage;

public class UploadProductImageRequest
{
    public Guid Id { get; set; }
    public required IFormFile File { get; set; }
}