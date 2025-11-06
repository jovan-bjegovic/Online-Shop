using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Products.SetProductImage;

namespace OnlineShop.Core.UseCases.Products.GetAll
{
    public class GetAllProductsUseCase(
        IProductRepository repository
    ) : IUseCase<GetAllProductsRequest, GetAllProductsResponse>
    {
        public async Task<GetAllProductsResponse> Execute(GetAllProductsRequest request)
        {
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
}