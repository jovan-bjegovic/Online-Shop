using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface ICategoryService
{
    List<Category> GetAll();
    Category? FindCategory(Guid id);
    bool RemoveCategory(Guid id);
    Category? CreateCategory(Category? newCategory);
    Category? UpdateCategory(Guid id, Category updated);
    bool CodeExists(string code, Guid? excludeId = null);
}