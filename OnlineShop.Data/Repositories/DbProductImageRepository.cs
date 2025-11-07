using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories
{
    public class DbProductImageRepository(AppDbContext context) : IProductImageRepository
    {
        public async Task<ProductImage?> FindByIdAsync(Guid id)
        {
            return await context.ProductImages
                .FirstOrDefaultAsync(img => img.Id == id);
        }

        public async Task AddAsync(ProductImage image)
        {
            await context.ProductImages.AddAsync(image);
        }

        public Task UpdateAsync(ProductImage image)
        {
            context.ProductImages.Update(image);
            return Task.CompletedTask;
        }
    }
}