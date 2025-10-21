using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories;

public class DbCategoryRepository(AppDbContext context) : BaseCategoryRepository, ICategoryRepository
{
    public List<Category> GetAll()
    {
        return context.Categories
            .Include(c => c.Subcategories)
            .ToList();
    }

    public Category? FindCategory(Guid id)
    {
        return context.Categories
            .Include(c => c.Subcategories)
            .FirstOrDefault(c => c.Id == id);
    }

    public Category CreateCategory(Category category)
    {
        context.Categories.Add(category);
        context.SaveChanges();
        
        return category;
    }

    public Category? UpdateCategory(Guid id, Category updated)
    {
        Category? existing = context.Categories.Find(id);
        if (existing == null)
        {
            return null;
        }

        context.Entry(existing).CurrentValues.SetValues(updated);
        context.SaveChanges();
        
        return existing;
    }

    public bool RemoveCategory(Guid id)
    {
        Category? category = context.Categories.Find(id);
        if (category == null)
        {
            return false;
        }

        context.Categories.Remove(category);
        context.SaveChanges();
        
        return true;
    }
}