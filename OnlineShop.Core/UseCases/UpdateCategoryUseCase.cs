using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases;

public class UpdateCategoryUseCase(ICategoryService service)
{
    public Category? Execute(Guid id, Category updatedCategory)
    {
        var existing = service.FindCategory(id);
        if (existing == null)
        {
            return null;
        }

        if (service.CodeExists(updatedCategory.Code, id))
        {
            throw new InvalidOperationException($"A category with code '{updatedCategory.Code}' already exists.");
        }

        return service.UpdateCategory(id, updatedCategory);
    }
}