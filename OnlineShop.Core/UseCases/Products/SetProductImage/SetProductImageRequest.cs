namespace OnlineShop.Core.UseCases.Products.SetProductImage;

public class SetProductImageRequest
{
    public Guid Id { get; init; }
    public required Guid ImageId { get; set; }
}