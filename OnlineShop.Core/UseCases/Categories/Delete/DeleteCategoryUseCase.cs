using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.Delete;

public class DeleteCategoryUseCase(IUnitOfWork unitOfWork)
    : IUseCase<DeleteCategoryRequest, DeleteCategoryResponse>
{
    public DeleteCategoryResponse Execute(DeleteCategoryRequest request)
    {
        Category? category = unitOfWork.Categories.FindCategory(request.Id);
        if (category == null)
        {
            return new DeleteCategoryResponse { Success = false };
        }

        unitOfWork.Categories.RemoveCategory(category);
        unitOfWork.CommitAsync().GetAwaiter().GetResult();

        return new DeleteCategoryResponse { Success = true };
    }
}