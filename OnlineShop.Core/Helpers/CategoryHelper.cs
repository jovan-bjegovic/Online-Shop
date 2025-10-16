using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Helpers;

public class CategoryHelper
{
    private readonly ICategoryRepository repository;

    public CategoryHelper(ICategoryRepository repository)
    {
        this.repository = repository;
    }

    public bool CodeExists(string code, Guid? excludeId = null)
    {
        var categories = repository.GetAll();
        return CodeExistsRecursive(categories, code, excludeId);
    }

    private static bool CodeExistsRecursive(List<Category> categories, string code, Guid? excludeId = null)
    {
        foreach (var category in categories.Where(c => !excludeId.HasValue || c.Id != excludeId.Value))
        {
            if (string.Equals(category.Code, code, StringComparison.OrdinalIgnoreCase))
                return true;

            if (CodeExistsRecursive(category.Subcategories, code, excludeId))
                return true;
        }
        return false;
    }
}