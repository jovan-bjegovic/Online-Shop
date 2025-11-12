using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.SetProductImage;

public class SetProductImageResponse
{
    public required Product Product { get; set; }
}