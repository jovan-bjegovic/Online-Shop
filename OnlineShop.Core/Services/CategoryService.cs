using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Services;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public List<Category> GetAll()
    {
        return repository.GetAll();
    }

    public Category? FindCategory(Guid id)
    {
        return repository.FindCategory(id);
    }
        
    public bool RemoveCategory(Guid id)
    {
        return repository.RemoveCategory(id);
    }
            
    public Category CreateCategory(Category newCategory)
    {
        if (CodeExists(newCategory.Code))
        {
            throw new ArgumentException($"A category with code '{newCategory.Code}' already exists.");
        }
            
        return repository.CreateCategory(newCategory);
    }

    public Category? UpdateCategory(Guid id, Category updated)
    {
        Category? existing = repository.FindCategory(id);
        if (existing == null)
        {
            return null;
        }

        if (CodeExists(updated.Code, id))
        {
            throw new InvalidOperationException($"A category with code '{updated.Code}' already exists.");
        }

        return repository.UpdateCategory(id, updated);
    }
    public bool CodeExists(string code, Guid? excludeId = null)
    {
        List<Category> categories = repository.GetAll();
            
        return CodeExistsRecursive(categories, code, excludeId );
    }

    private static bool CodeExistsRecursive(List<Category> categories, string code, Guid? excludeId = null)
    {
        foreach (Category category in categories.Where(category => !excludeId.HasValue || category.Id != excludeId.Value))
        {
            if (string.Equals(category.Code, code, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (CodeExistsRecursive(category.Subcategories, code, excludeId))
            {
                return true;
            }
        }

        return false;
    }
}