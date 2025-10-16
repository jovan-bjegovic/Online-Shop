using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface ICategoryRepository
{
    List<Category> GetAll();
    Category? FindCategory(Guid id);
    bool RemoveCategory(Guid id);
    Category CreateCategory(Category category);
    Category? UpdateCategory(Guid id, Category updated);
}