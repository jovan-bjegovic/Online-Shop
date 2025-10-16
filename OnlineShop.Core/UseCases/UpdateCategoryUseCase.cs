using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases;

public class UpdateCategoryUseCase(ICategoryService service)
    : IUseCase<(Guid id, Category category), Category?>
{
    public Category? Execute((Guid id, Category category) input)
    {
        var (id, updatedCategory) = input;

        Category? existing = service.FindCategory(id);
        if (existing == null)
        {
            return null;
        }

        if (service.CodeExists(updatedCategory.Code, id))
        {
            throw new InvalidOperationException(
                $"A category with code '{updatedCategory.Code}' already exists."
            );
        }

        return service.UpdateCategory(id, updatedCategory);
    }
}
