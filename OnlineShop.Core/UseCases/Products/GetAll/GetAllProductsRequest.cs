namespace OnlineShop.Core.UseCases.Products.GetAll;

public class GetAllProductsRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}