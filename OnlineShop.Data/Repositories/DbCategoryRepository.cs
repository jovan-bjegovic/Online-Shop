using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories;

public class DbCategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<List<Category>> GetAllAsync()
    {
        return await context.Categories
            .Where(c => c.ParentCategoryId == null)
            .Include(c => c.Subcategories)
            .ToListAsync();
    }

    public async Task<Category?> FindCategoryAsync(Guid id)
    {
        return await context.Categories
            .Include(c => c.Subcategories)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task CreateCategoryAsync(Category category)
    {
        await context.Categories.AddAsync(category);
    }

    public Task UpdateCategoryAsync(Category category)
    {
        context.Categories.Update(category);
        
        return Task.CompletedTask;
    }

    public Task RemoveCategoryAsync(Category category)
    {
        category.IsDeleted = true;
        category.DeletedAt = DateTime.UtcNow;
        context.Categories.Update(category);
        return Task.CompletedTask;
    }
    
    public async Task<List<Category>> GetAndRemoveExpiredAsync(DateTime cutoffDate)
    {
        var expiredCategories = await context.Categories
            .IgnoreQueryFilters()
            .Where(c => c.IsDeleted && c.DeletedAt <= cutoffDate)
            .ToListAsync();

        if (expiredCategories.Count > 0)
        {
            await context.Categories
                .IgnoreQueryFilters()
                .Where(c => expiredCategories.Select(x => x.Id).Contains(c.Id))
                .ExecuteDeleteAsync();
        }

        return expiredCategories;
    }

    
    public async Task<bool> CodeExistsAsync(string code, Guid? excludeId = null)
    {
        var query = context.Categories.AsQueryable();

        if (excludeId.HasValue)
        {
            query = query.Where(c => c.Id != excludeId.Value);
        }

        return await query.AnyAsync(c => c.Code.ToLower() == code.ToLower());
    }

    public async Task<bool> IsCircularParentAsync(Guid categoryId, Guid newParentId)
    {
        Guid parentId = newParentId;

        while (true)
        {
            if (parentId == categoryId)
            {
                return true;
            }

            Guid id = parentId;
            Guid? parent = await context.Categories
                .Where(c => c.Id == id)
                .Select(c => c.ParentCategoryId)
                .FirstOrDefaultAsync();

            if (!parent.HasValue)
            {
                break;
            }
            parentId = parent.Value;
        }

        return false;
    }
}