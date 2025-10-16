using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases;

public class GetCategoryByIdUseCase(ICategoryService service)
{
    public Category? Execute(Guid id)
    {
        return service.FindCategory(id);
    }
}