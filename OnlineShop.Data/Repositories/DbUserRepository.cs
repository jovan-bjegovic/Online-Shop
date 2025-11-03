using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories;

public class DbUserRepository(AppDbContext db) : IUserRepository
{
    public Task<User?> GetUserByUsernameAsync(string username)
    {
        return db.Users.SingleOrDefaultAsync(u => u.Username == username);
    }

    public Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        return db.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public Task UpdateUserAsync(User user)
    {
        db.Users.Update(user);
        
        return Task.CompletedTask;
    }
}
