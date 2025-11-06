namespace OnlineShop.Core.UseCases.Products.SetProductImage;

public class SetProductImageRequest
{
    public Guid ProductId { get; set; }
    public string ImagePath { get; set; }
}
