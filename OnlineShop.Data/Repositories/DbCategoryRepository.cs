using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories;

public class DbCategoryRepository(AppDbContext context) : ICategoryRepository
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

    public void CreateCategory(Category category)
    {
        context.Categories.Add(category);
    }

    public void UpdateCategory(Category category)
    {
        context.Categories.Update(category);
    }

    public void RemoveCategory(Category category)
    {
        context.Categories.Remove(category);
    }
}