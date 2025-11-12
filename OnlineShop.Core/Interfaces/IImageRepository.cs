using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface IImageRepository
{
    Task<Image?> FindByIdAsync(Guid id);
    Task AddAsync(Image image);
}