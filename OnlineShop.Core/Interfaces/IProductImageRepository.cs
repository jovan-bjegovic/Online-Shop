using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface IProductImageRepository
{
    Task<ProductImage?> FindByIdAsync(Guid id);
    Task AddAsync(ProductImage image);
    Task UpdateAsync(ProductImage image);
}