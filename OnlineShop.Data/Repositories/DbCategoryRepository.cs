using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories;

public class DbCategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<List<Category>> GetAllAsync()
    {
        List<Category> allCategories = await context.Categories.ToListAsync();

        List<Category> rootCategories = allCategories
            .Where(c => c.ParentCategoryId == null)
            .ToList();

        foreach (Category root in rootCategories)
        {
            root.Subcategories = BuildSubcategories(root.Id, allCategories);
        }

        return rootCategories;
    }

    private static List<Category> BuildSubcategories(Guid parentId, List<Category> allCategories)
    {
        List<Category> children = allCategories
            .Where(c => c.ParentCategoryId == parentId)
            .ToList();

        foreach (Category child in children)
        {
            child.Subcategories = BuildSubcategories(child.Id, allCategories);
        }

        return children;
    }

    public async Task<Category?> FindCategoryAsync(Guid id)
    {
        List<Category> allCategories = await context.Categories.ToListAsync();

        Category? category = allCategories.FirstOrDefault(c => c.Id == id);
        if (category == null)
            return null;

        category.Subcategories = BuildSubcategories(category.Id, allCategories);

        return category;
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
        context.Categories.Remove(category);
        
        return Task.CompletedTask;
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