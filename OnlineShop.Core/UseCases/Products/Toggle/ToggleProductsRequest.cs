namespace OnlineShop.Core.UseCases.Products.Toggle;

public class ToggleProductsRequest
{
    public List<Guid> Ids { get; set; } = [];
}