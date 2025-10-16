using OnlineShop.Core.Interfaces;

namespace OnlineShop.Core.UseCases;

public class DeleteCategoryUseCase(ICategoryService service)
{
    public bool Execute(Guid id)
    {
        return service.RemoveCategory(id);
    }
}