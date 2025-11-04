using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.Services;

namespace OnlineShop.Core.UseCases.Auth.Login;

public class LoginUseCase(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenService tokenService
) : IUseCase<LoginRequest, LoginResponse>
{
    public async Task<LoginResponse> Execute(LoginRequest request)
    {
        User? user = await userRepository.GetUserByUsernameAsync(request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        string accessToken = tokenService.GenerateAccessToken(user);
        string refreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        user.CreatedAt = DateTime.SpecifyKind(user.CreatedAt, DateTimeKind.Utc);
        
        await userRepository.UpdateUserAsync(user);
        await unitOfWork.CommitAsync();

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}