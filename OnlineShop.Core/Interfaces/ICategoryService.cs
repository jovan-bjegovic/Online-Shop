using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface ICategoryService
{
    List<Category> GetAll();
    Category? FindCategory(int id);
    bool RemoveCategory(int id);
    Category CreateCategory(Category newCategory);
    Category? UpdateCategory(int id, Category updated);
}