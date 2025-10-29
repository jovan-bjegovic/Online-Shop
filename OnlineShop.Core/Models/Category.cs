namespace OnlineShop.Core.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? ParentCategoryId { get; set; }
    public List<Category> Subcategories { get; init; } = [];
}