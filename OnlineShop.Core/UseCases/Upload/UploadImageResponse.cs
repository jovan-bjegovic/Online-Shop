namespace OnlineShop.Core.UseCases.Upload;

public class UploadImageResponse
{
    public Guid Id { get; init; } 
    public string? FileName { get; init; }
    public string? FilePath { get; init; }
    public long Size { get; init; } 
    public DateTime CreatedAt { get; init; }
}