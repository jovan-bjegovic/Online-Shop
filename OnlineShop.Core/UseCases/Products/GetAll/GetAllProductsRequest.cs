namespace OnlineShop.Core.UseCases.Products.GetAll;

public class GetAllProductsRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}