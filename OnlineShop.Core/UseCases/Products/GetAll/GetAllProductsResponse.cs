using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.GetAll;

public class GetAllProductsResponse
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public List<Product> Products { get; set; } = [];
}