namespace OnlineShop.Core.UseCases.Products.UploadImage;

public class UploadProductImageResponse
{
    public Guid Id { get; set; }
    public string FileName { get; set; } =  string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long Size { get; set; }
    public DateTime CreatedAt { get; set; }
}