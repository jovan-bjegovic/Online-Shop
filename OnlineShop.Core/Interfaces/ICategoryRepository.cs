using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category? FindCategory(int id);
        bool RemoveCategory(int id);
    }
    
    public interface IWritableCategoryRepository : ICategoryRepository
    {
        Category CreateCategory(Category category);
        Category? UpdateCategory(int id, Category updated);
    }
}