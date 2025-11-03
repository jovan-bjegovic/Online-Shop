using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.DTOs;
using OnlineShop.Core.Models;
using OnlineShop.Data;
using OnlineShop.Services;

namespace OnlineShop.Controller;

[ApiController]
[Route("[controller]")]
public class AuthController(
    TokenService tokenService, 
    AppDbContext dbContext
    ) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        User? user = await dbContext.Users.SingleOrDefaultAsync(u => u.Username == request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized(new Response<object>(
                StatusCodes.Status401Unauthorized,
                "Invalid username or password"
            ));
        }

        string accessToken = tokenService.GenerateAccessToken(user);
        string refreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await dbContext.SaveChangesAsync();

        return Ok(new Response<object>(
            StatusCodes.Status200OK,
            "Login successful",
            new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            }
        ));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        User? user = await dbContext.Users.SingleOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);
        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            return Unauthorized(new Response<object>(
                StatusCodes.Status401Unauthorized,
                "Invalid or expired refresh token"
            ));
        }

        string newAccessToken = tokenService.GenerateAccessToken(user);
        string newRefreshToken = tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await dbContext.SaveChangesAsync();

        return Ok(new Response<object>(
            StatusCodes.Status200OK,
            "Refresh login successful",
            new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            }
        ));
    }
}
