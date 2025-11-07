namespace OnlineShop.Core.UseCases.Upload;

public class UploadFileResponse
{
    public Guid Id { get; set; } 
    public string FileName { get; set; } = default!;
    public string FilePath { get; set; } = default!;
    public long Size { get; set; } 
    public DateTime CreatedAt { get; set; }
}