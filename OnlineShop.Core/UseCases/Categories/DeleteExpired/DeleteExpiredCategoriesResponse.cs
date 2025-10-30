namespace OnlineShop.Core.UseCases.Categories.DeleteExpired;

public class DeleteExpiredCategoriesResponse
{
    public DateTime CutoffDate { get; set; }
    public List<DeletedCategoryInfo> DeletedCategories { get; set; } = [];
    public int Count => DeletedCategories.Count;
}

public class DeletedCategoryInfo
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime? DeletedAt { get; set; }
}