using Microsoft.Extensions.Options;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.Options;
using OnlineShop.Core.UseCases.Products.SetProductImage;

namespace OnlineShop.Core.UseCases.Auth.Refresh;

public class RefreshTokenUseCase(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenService tokenService,
    IOptions<JwtOptions> jwtOptions
) : IUseCase<RefreshTokenRequest, RefreshTokenResponse>
{
    public async Task<RefreshTokenResponse> Execute(RefreshTokenRequest request)
    {
        User? user = await userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);

        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }

        string newAccessToken = tokenService.GenerateAccessToken(user);
        string newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpirationDays);
        
        await userRepository.UpdateUserAsync(user);
        await unitOfWork.CommitAsync();

        return new RefreshTokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}
