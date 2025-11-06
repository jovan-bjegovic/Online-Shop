using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace OnlineShop.Core.UseCases.Products.DeleteExpired;

public class DeleteExpiredProductsUseCase(
    IProductRepository repository,
    IUnitOfWork unitOfWork)
    : IUseCase<DeleteExpiredProductsRequest, DeleteExpiredProductsResponse>
{
    public async Task<DeleteExpiredProductsResponse> Execute(DeleteExpiredProductsRequest request)
    {
        List<Product> expiredProducts = await repository.GetExpiredAsync(request.CutoffDate);

        List<DeletedProductInfo> deletedInfos = expiredProducts.Select(p => new DeletedProductInfo
        {
            Id = p.Id,
            Title = p.Title,
            Sku = p.Sku,
            DeletedAt = p.DeletedAt
        }).ToList();

        await repository.DeleteProductsAsync(expiredProducts);

        await unitOfWork.CommitAsync();

        return new DeleteExpiredProductsResponse
        {
            CutoffDate = request.CutoffDate,
            DeletedProducts = deletedInfos
        };
    }
}