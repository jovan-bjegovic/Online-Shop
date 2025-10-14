namespace OnlineShop.Core.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Code { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public Guid? ParentCategoryId { get; set; }
    public List<Category> Subcategories { get; set; } = new();
}