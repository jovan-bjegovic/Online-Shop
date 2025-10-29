using OnlineShop.Core.Interfaces;

namespace OnlineShop.Core.Models;

public class Category : ISoftDelete
{
    public Guid Id { get; init; }
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? ParentCategoryId { get; set; }
    public List<Category> Subcategories { get; init; } = [];
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}