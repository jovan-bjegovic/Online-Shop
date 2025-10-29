using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> FindCategoryAsync(Guid id);
    Task RemoveCategoryAsync(Category category);
    Task CreateCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task<bool> CodeExistsAsync(string code, Guid? excludeId = null);
    Task<bool> IsCircularParentAsync(Guid categoryId, Guid newParentId);
    Task<int> RemoveExpiredAsync(DateTime threshold);
}