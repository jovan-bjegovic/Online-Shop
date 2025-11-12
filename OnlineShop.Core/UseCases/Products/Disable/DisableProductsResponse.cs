using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.Disable;

public class DisableProductsResponse
{
    public List<ProductStatusItem> Products { get; set; } = [];
}
