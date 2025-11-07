using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories
{
    public class DbImageRepository(AppDbContext context) : IImageRepository
    {
        public async Task<Image?> FindByIdAsync(Guid id)
        {
            return await context.Images
                .FirstOrDefaultAsync(img => img.Id == id);
        }

        public async Task AddAsync(Image image)
        {
            await context.Images.AddAsync(image);
        }
    }
}