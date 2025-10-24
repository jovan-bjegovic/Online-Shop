using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.Get;

public class GetCategoryUseCase(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork)
    : IUseCase<GetCategoryRequest, GetCategoryResponse>
{
    public async Task<GetCategoryResponse> Execute(GetCategoryRequest request)
    {
        Category? category = await repository.FindCategory(request.Id);

        return new GetCategoryResponse
        {
            Category = category
        };
    }
}