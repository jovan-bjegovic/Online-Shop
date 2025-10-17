using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.GetAll;

public class GetAllCategoriesUseCase(ICategoryRepository repository) 
    : IUseCase<GetAllCategoriesResponse>
{
    public GetAllCategoriesResponse Execute()
    {
        List<Category> categories = repository.GetAll();
        
        return new GetAllCategoriesResponse { Categories = categories };
    }
}