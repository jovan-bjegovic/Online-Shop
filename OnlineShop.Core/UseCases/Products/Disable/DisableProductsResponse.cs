namespace OnlineShop.Core.UseCases.Products.Disable;

public class DisableProductsResponse
{
    public List<ProductStatusItem> Products { get; set; } = [];
}

public record ProductStatusItem
{
    public Guid Id { get; set; }
    public bool Enabled { get; set; }
}