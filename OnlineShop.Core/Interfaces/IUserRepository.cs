using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
    Task UpdateUserAsync(User user);
}