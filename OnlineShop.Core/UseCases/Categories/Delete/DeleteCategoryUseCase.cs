using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.Delete;

public class DeleteCategoryUseCase(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork)
    : IUseCase<DeleteCategoryRequest, DeleteCategoryResponse>
{
    public DeleteCategoryResponse Execute(DeleteCategoryRequest request)
    {
        Category? category = repository.FindCategory(request.Id);
        if (category == null)
        {
            return new DeleteCategoryResponse { Success = false };
        }

        repository.RemoveCategory(category);
        unitOfWork.CommitAsync().GetAwaiter().GetResult();

        return new DeleteCategoryResponse { Success = true };
    }
}