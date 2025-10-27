using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories;

public class DbCategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<List<Category>> GetAllAsync()
    {
        return await context.Categories
            .Include(c => c.Subcategories)
            .ToListAsync();
    }

    public async Task<Category?> FindCategoryAsync(Guid id)
    {
        return await context.Categories
            .Include(c => c.Subcategories)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public Task CreateCategoryAsync(Category category)
    {
        context.Categories.Add(category);
        return Task.CompletedTask;
    }

    public Task UpdateCategoryAsync(Category category)
    {
        context.Categories.Update(category);
        return Task.CompletedTask;
    }

    public Task RemoveCategoryAsync(Category category)
    {
        context.Categories.Remove(category);
        return Task.CompletedTask;
    }
}