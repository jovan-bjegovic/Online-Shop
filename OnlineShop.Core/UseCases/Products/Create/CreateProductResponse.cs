using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.Create;

public class CreateProductResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string ShortDescription { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public int Price { get; set; }
    public Guid? ImageId { get; set; }
    public bool Enabled { get; set; } = true;
    public bool Featured { get; set; } = false;
}