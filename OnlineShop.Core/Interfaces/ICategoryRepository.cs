using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category? FindCategory(Guid id);
        bool RemoveCategory(Guid id);
        bool CodeExists(string code);
        Category CreateCategory(CategoryDto categoryDto);
        Category? UpdateCategory(Guid id, CategoryDto updated);
    }
}