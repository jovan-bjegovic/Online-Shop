using OnlineShop.Core.Interfaces;

namespace OnlineShop.Core.UseCases.Categories.Delete;

public class DeleteCategoryUseCase(ICategoryRepository repository) 
    : IUseCase<DeleteCategoryRequest, DeleteCategoryResponse>
{
    public DeleteCategoryResponse Execute(DeleteCategoryRequest request)
    {
        bool removed = repository.RemoveCategory(request.Id);
        
        return new DeleteCategoryResponse { Success = removed };
    }
}