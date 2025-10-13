using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category? FindCategory(int id);
        bool RemoveCategory(int id);
        bool CodeExists(string code, int excludeId = -1);
        Category CreateCategory(Category category);
        Category? UpdateCategory(int id, Category updated);
    }
}