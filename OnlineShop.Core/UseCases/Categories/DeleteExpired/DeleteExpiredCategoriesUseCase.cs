using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Products.SetProductImage;

namespace OnlineShop.Core.UseCases.Categories.DeleteExpired;

public class DeleteExpiredCategoriesUseCase(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork)
    : IUseCase<DeleteExpiredCategoriesRequest, DeleteExpiredCategoriesResponse>
{
    public async Task<DeleteExpiredCategoriesResponse> Execute(DeleteExpiredCategoriesRequest request)
    {
        List<Category> expiredCategories = await repository.GetExpiredAsync(request.CutoffDate);

        List<DeletedCategoryInfo> deletedInfos = expiredCategories.Select(c => new DeletedCategoryInfo
        {
            Id = c.Id,
            Title = c.Title,
            Code = c.Code,
            DeletedAt = c.DeletedAt
        }).ToList();
        
        foreach (Category c in expiredCategories)
        {
            await repository.RemoveCategoryAsync(c);
        }
        
        await unitOfWork.CommitAsync();

        return new DeleteExpiredCategoriesResponse
        {
            CutoffDate = request.CutoffDate,
            DeletedCategories = deletedInfos
        };
    }

}