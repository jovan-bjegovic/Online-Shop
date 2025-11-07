using OnlineShop.Core.Interfaces;

namespace OnlineShop.Core.Models;

public class Product: ISoftDelete
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string ShortDescription { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public int Price { get; set; }
    public Guid? ImageId { get; set; }  
    public bool Enabled { get; set; } = true;
    public bool Featured { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}