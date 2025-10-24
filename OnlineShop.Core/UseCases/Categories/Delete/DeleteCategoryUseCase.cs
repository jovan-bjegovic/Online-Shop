using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.Delete;

public class DeleteCategoryUseCase(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork)
    : IUseCase<DeleteCategoryRequest, DeleteCategoryResponse>
{
    public async Task<DeleteCategoryResponse> Execute(DeleteCategoryRequest request)
    {
        Category? category = await repository.FindCategory(request.Id);
        if (category == null)
        {
            return new DeleteCategoryResponse { Success = false };
        }

        await repository.RemoveCategory(category);
        await unitOfWork.CommitAsync();

        return new DeleteCategoryResponse { Success = true };
    }
}