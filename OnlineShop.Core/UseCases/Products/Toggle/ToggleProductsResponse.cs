namespace OnlineShop.Core.UseCases.Products.Toggle;

public class ToggleProductsResponse
{
    public List<ToggleProductItem> Products { get; set; }
}

public class ToggleProductItem
{
    public Guid Id { get; set; }
    public bool Enabled { get; set; }
}
