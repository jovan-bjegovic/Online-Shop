using Microsoft.AspNetCore.Http;

namespace OnlineShop.Core.UseCases.Products.UploadImage;

public class UploadProductImageRequest
{
    public string Sku { get; set; }
    public IFormFile File { get; set; }
}
