using Microsoft.Extensions.Options;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.Options;
using OnlineShop.Core.UseCases.Products.SetProductImage;

namespace OnlineShop.Core.UseCases.Auth.Login;

public class GenerateTokenUseCase(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ITokenService tokenService,
    IPasswordHasher passwordHasher,
    IOptions<JwtOptions> jwtOptions
) : IUseCase<GenerateTokenRequest, GenerateTokenResponse>
{
    public async Task<GenerateTokenResponse> Execute(GenerateTokenRequest request)
    {
        User? user = await userRepository.GetUserByUsernameAsync(request.Username);
        if (user == null || !passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        string accessToken = tokenService.GenerateAccessToken(user);
        string refreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshTokenExpirationDays);
        
        await userRepository.UpdateUserAsync(user);
        await unitOfWork.CommitAsync();

        return new GenerateTokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}