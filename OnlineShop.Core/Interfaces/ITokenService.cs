using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}