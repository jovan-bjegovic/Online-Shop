namespace OnlineShop.Core.UseCases.Products.Enable;

public class EnableProductsResponse
{
    public List<ProductStatusItem> Products { get; set; } = [];
}

public record ProductStatusItem
{
    public Guid Id { get; set; }
    public bool Enabled { get; set; }
}