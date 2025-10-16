using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases;

public class GetAllCategoriesUseCase(ICategoryService service)
{
    public List<Category> Execute()
    {
        return service.GetAll();
    }
}