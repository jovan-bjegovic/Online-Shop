using OnlineShop.Core.Interfaces;
using OnlineShop.Core.UseCases.Requests;
using OnlineShop.Core.UseCases.Responses;

namespace OnlineShop.Core.UseCases;

public class DeleteCategoryUseCase(ICategoryRepository repository) 
    : IUseCase<DeleteCategoryRequest, DeleteCategoryResponse>
{
    public DeleteCategoryResponse Execute(DeleteCategoryRequest request)
    {
        bool removed = repository.RemoveCategory(request.Id);
        return new DeleteCategoryResponse { Success = removed };
    }
}