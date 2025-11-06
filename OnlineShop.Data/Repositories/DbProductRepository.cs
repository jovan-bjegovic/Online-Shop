using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories;

public class DbProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<Product?> FindByIdAsync(Guid id)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<Product?> FindBySkuAsync(string sku)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Sku == sku);
    }

    public async Task<List<Product>> GetAllPaginatedAsync(int page, int pageSize)
    {
        return await context.Products
            .OrderBy(p => p.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await context.Products.CountAsync();
    }

    public async Task CreateProductAsync(Product product)
    {
        await context.Products.AddAsync(product);
    }

    public Task UpdateProductAsync(Product product)
    {
        context.Products.Update(product);
        
        return Task.CompletedTask;
    }

    public Task UpdateProductsAsync(List<Product> products)
    {
        context.Products.UpdateRange(products);
        
        return Task.CompletedTask;
    }

    public Task DeleteProductsAsync(List<Product> products)
    {
        context.Products.RemoveRange(products);
        
        return Task.CompletedTask;
    }
    public async Task<bool> SkuExistsAsync(string sku)
    {
        return await context.Products.AnyAsync(p => p.Sku == sku);
    }

    public async Task<bool> CategoryExistsAsync(Guid categoryId)
    {
        return await context.Categories.AnyAsync(c => c.Id == categoryId);
    }

    public async Task<List<Product>> FindByIdsAsync(List<Guid> ids)
    {
        return await context.Products
            .Where(p => ids.Contains(p.Id))
            .ToListAsync();
    }
}