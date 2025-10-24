using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories;

public class DbCategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<List<Category>> GetAll()
    {
        return await context.Categories
            .Include(c => c.Subcategories)
            .ToListAsync();
    }

    public async Task<Category?> FindCategory(Guid id)
    {
        return await context.Categories
            .Include(c => c.Subcategories)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task CreateCategory(Category category)
    {
        context.Categories.Add(category);
        await Task.CompletedTask;
    }

    public async Task UpdateCategory(Category category)
    {
        context.Categories.Update(category);
        await Task.CompletedTask;
    }

    public async Task RemoveCategory(Category category)
    {
        context.Categories.Remove(category);
        await Task.CompletedTask;
    }
}