using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.GetAll;

public class GetAllCategoriesUseCase(
    ICategoryRepository repository)
    : IUseCase<GetAllCategoriesResponse>
{
    public async Task<GetAllCategoriesResponse> Execute()
    {
        List<Category> categories = await repository.GetAllAsync();

        return new GetAllCategoriesResponse
        {
            Categories = categories
        };
    }
}
