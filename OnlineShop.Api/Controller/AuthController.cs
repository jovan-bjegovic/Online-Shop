using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.DTOs;
using OnlineShop.Core.Models;
using OnlineShop.Services;

namespace OnlineShop.Controller;

[ApiController]
[Route("[controller]")]
public class AuthController(TokenService tokenService) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            if (request.Username == "admin" && request.Password == "1234")
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = request.Username,
                    Role = "Admin"
                };

                var accessToken = tokenService.GenerateAccessToken(user);
                var refreshToken = tokenService.GenerateRefreshToken();

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

            return Unauthorized(new Response<object>(
                StatusCodes.Status401Unauthorized,
                "Invalid username or password"
            ));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response<object>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while logging in",
                    ex.Message
                ));
        }
    }
}
