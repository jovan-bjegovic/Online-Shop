namespace OnlineShop.Core.UseCases.Products.DeleteExpired;

public class DeleteExpiredProductsResponse
{
    public DateTime CutoffDate { get; set; }
    public List<DeletedProductInfo> DeletedProducts { get; set; } = [];
    public int Count => DeletedProducts.Count;
}

public class DeletedProductInfo
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public DateTime? DeletedAt { get; set; }
}