using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Products.SetProductImage;

namespace OnlineShop.Core.UseCases.Categories.Get;

public class GetCategoryUseCase(
    ICategoryRepository repository)
    : IUseCase<GetCategoryRequest, GetCategoryResponse>
{
    public async Task<GetCategoryResponse> Execute(GetCategoryRequest request)
    {
        Category? category = await repository.FindCategoryAsync(request.Id);

        return new GetCategoryResponse
        {
            Category = category
        };
    }
}