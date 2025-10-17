using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.Get;

public class GetCategoryUseCase(ICategoryRepository repository) : IUseCase<GetCategoryRequest, GetCategoryResponse>
{
    public GetCategoryResponse Execute(GetCategoryRequest request)
    {
        Category? category = repository.FindCategory(request.Id);
        
        return new GetCategoryResponse { Category = category };
    }
}
