using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAll();
    Task<Category?> FindCategory(Guid id);
    Task RemoveCategory(Category category);
    Task CreateCategory(Category category);
    Task UpdateCategory(Category category);

}