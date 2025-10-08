namespace OnlineShop.Models;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Code { get; set; } = "";
    public string Description { get; set; } = "";

    public int? ParentCategoryId { get; set; }

    public List<Category> Subcategories { get; set; } = new();
}