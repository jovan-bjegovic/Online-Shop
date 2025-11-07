using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
namespace OnlineShop.Core.UseCases.Products.GetAll;

public class GetAllProductsUseCase(
    IProductRepository repository
) : IUseCase<GetAllProductsRequest, GetAllProductsResponse>
{
    public async Task<GetAllProductsResponse> Execute(GetAllProductsRequest request)
    {
        if (request.Page <= 0)
        {
            throw new ArgumentException("Page must be greater than 0.");
        }

        if (request.PageSize <= 0)
        {
            throw new ArgumentException("PageSize must be greater than 0.");
        }
        
        int totalItems = await repository.CountAsync();
        int totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);

        List<Product> products = await repository.GetAllPaginatedAsync(request.Page, request.PageSize);

        return new GetAllProductsResponse
        {
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = totalItems,
            TotalPages = totalPages,
            Products = products
        };
    }
}