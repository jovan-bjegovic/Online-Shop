using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Responses;

namespace OnlineShop.Core.UseCases;

public class GetAllCategoriesUseCase(ICategoryRepository repository) 
    : IUseCase<GetAllCategoriesResponse>
{
    public GetAllCategoriesResponse Execute()
    {
        List<Category> categories = repository.GetAll();
        return new GetAllCategoriesResponse { Categories = categories };
    }
}