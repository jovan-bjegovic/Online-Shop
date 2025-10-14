using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface ICategoryService
{
    List<Category> GetAll();
    Category? FindCategory(Guid id);
    bool RemoveCategory(Guid id);
    Category? CreateCategory(CategoryDto newCategory);
    Category? UpdateCategory(Guid id, CategoryDto updated);
    bool CodeExists(string code, Guid? excludeId = null);
}