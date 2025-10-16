using OnlineShop.Core.Interfaces;

namespace OnlineShop.Core.UseCases;

public class DeleteCategoryUseCase(ICategoryService service)
    : IUseCase<Guid, bool>
{
    public bool Execute(Guid id)
    {
        return service.RemoveCategory(id);
    }
}