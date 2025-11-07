namespace OnlineShop.Core.Models;

public class Image : BaseEntity
{
    public string FileName { get; init; } = string.Empty;
    public string FilePath { get; init; } = string.Empty;
    public long Size { get; init; }
}