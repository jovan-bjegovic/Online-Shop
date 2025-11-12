namespace OnlineShop.Core.UseCases.Products.SetProductImage;

public class SetProductImageRequest
{
    public Guid Id { get; set; }
    public required Guid ImageId { get; init; }
}