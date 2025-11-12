using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.Enable;

public class EnableProductsResponse
{
    public List<ProductStatusItem> Products { get; set; } = [];
}