using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases;

public class GetAllCategoriesUseCase(ICategoryService service)
    : IUseCase<object?, List<Category>>
{
    public List<Category> Execute(object? input)
    {
        return service.GetAll();
    }
}
