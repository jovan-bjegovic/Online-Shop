using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Products.Delete;

public class DeleteProductsUseCase(
    IProductRepository repository,
    IUnitOfWork unitOfWork
) : IUseCase<DeleteProductsRequest, DeleteProductsResponse>
{
    public async Task<DeleteProductsResponse> Execute(DeleteProductsRequest request)
    {
        if (request.Ids == null || request.Ids.Count == 0)
        {
            throw new ArgumentException("No product IDs provided.");
        }

        List<Product> products = await repository.FindByIdsAsync(request.Ids);

        if (products.Count == 0)
        {
            return new DeleteProductsResponse { Success = false };
        }
        
        await repository.SoftDeleteProductsAsync(products);
        await unitOfWork.CommitAsync();

        return new DeleteProductsResponse { Success = true };
    }
}
