using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface ICategoryRepository
{
    List<Category> GetAll();
    Category? FindCategory(Guid id);
    void RemoveCategory(Category category);
    void CreateCategory(Category category);
    void UpdateCategory(Category category);

}