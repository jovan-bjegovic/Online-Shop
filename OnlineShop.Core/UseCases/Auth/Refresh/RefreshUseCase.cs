using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.Services;
using OnlineShop.Core.UseCases.Auth.Login;

namespace OnlineShop.Core.UseCases.Auth.Refresh;

public class RefreshUseCase(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenService tokenService
) : IUseCase<RefreshRequest, LoginResponse>
{
    public async Task<LoginResponse> Execute(RefreshRequest request)
    {
        User? user = await userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);

        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }

        string newAccessToken = tokenService.GenerateAccessToken(user);
        string newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        user.CreatedAt = DateTime.SpecifyKind(user.CreatedAt, DateTimeKind.Utc);
        
        await userRepository.UpdateUserAsync(user);
        await unitOfWork.CommitAsync();

        return new LoginResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}
