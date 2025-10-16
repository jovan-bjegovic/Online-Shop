using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases;

public class CreateCategoryUseCase(ICategoryService service)
{
    public Category Execute(Category newCategory)
    {
        if (service.CodeExists(newCategory.Code))
        {
            throw new ArgumentException($"A category with code '{newCategory.Code}' already exists.");
        }

        return service.CreateCategory(newCategory);
    }
}