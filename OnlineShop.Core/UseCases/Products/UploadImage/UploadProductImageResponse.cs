namespace OnlineShop.Core.UseCases.Products.UploadImage;

public class UploadProductImageResponse
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long Size { get; set; }
    public DateTime CreatedAt { get; set; }
}