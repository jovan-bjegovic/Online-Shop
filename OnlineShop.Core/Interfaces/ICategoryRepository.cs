using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category? FindCategory(Guid id);
        bool RemoveCategory(Guid id);
        Category CreateCategory(CategoryDto categoryDto);
        Category? UpdateCategory(Guid id, CategoryDto updated);
    }
}