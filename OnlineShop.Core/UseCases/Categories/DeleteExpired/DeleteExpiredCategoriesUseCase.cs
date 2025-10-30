using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.DeleteExpired;

public class DeleteExpiredCategoriesUseCase(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork)
    : IUseCase<DeleteExpiredCategoriesRequest, DeleteExpiredCategoriesResponse>
{
    public async Task<DeleteExpiredCategoriesResponse> Execute(DeleteExpiredCategoriesRequest request)
    {
        List<Category> expiredCategories = await repository.GetAndRemoveExpiredAsync(request.CutoffDate);

        await unitOfWork.CommitAsync();

        List<DeletedCategoryInfo> deletedInfos = expiredCategories.Select(c => new DeletedCategoryInfo
        {
            Id = c.Id,
            Title = c.Title,
            Code = c.Code,
            DeletedAt = c.DeletedAt
        }).ToList();

        return new DeleteExpiredCategoriesResponse
        {
            CutoffDate = request.CutoffDate,
            DeletedCategories = deletedInfos
        };
    }

}