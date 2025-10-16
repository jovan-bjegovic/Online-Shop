namespace OnlineShop.DTOs;

public class CategoryDto
{
    public string Title { get; init; } = string.Empty;
    public string Code { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Guid? ParentCategoryId { get; init; }
}