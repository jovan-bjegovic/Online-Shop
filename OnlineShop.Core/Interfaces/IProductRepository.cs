using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface IProductRepository
{
    Task<Product?> FindByIdAsync(Guid id);
    Task<Product?> FindBySkuAsync(string sku);
    Task<List<Product>> GetAllPaginatedAsync(int page, int pageSize);
    Task<List<Product>> GetExpiredAsync(DateTime cutoffDate);
    Task<int> CountAsync();
    Task CreateProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task UpdateProductsAsync(List<Product> products);
    Task SoftDeleteProductsAsync(List<Product> products);
    Task DeleteProductsAsync(List<Product> products);
    Task<bool> SkuExistsAsync(string sku);
    Task<bool> CategoryExistsAsync(Guid categoryId);
    Task<List<Product>> FindByIdsAsync(List<Guid> ids);
}