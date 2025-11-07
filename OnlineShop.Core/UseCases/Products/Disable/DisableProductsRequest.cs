namespace OnlineShop.Core.UseCases.Products.Disable;

public class DisableProductsRequest
{
    public List<Guid> Ids { get; set; } = [];
}