namespace OnlineShop.Core.UseCases.Products.SetProductImage;

public class SetProductImageRequest
{
    public Guid Id { get; set; }
    public string Image { get; set; } = String.Empty;
}
