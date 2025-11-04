using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Auth.Login;
using OnlineShop.Core.UseCases.Auth.Refresh;

namespace OnlineShop.Controller;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> GenerateToken(
        [FromBody] GenerateTokenRequest request,
        [FromServices] IUseCase<GenerateTokenRequest, GenerateTokenResponse> useCase
        )
    {
        try
        {
            GenerateTokenResponse response = await useCase.Execute(request);
            
            return Ok(new Response<GenerateTokenResponse>(
                StatusCodes.Status200OK, 
                "Login successful", response
                ));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new Response<object>(
                StatusCodes.Status401Unauthorized,
                ex.Message
                ));
        }
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenRequest request,
        [FromServices] IUseCase<RefreshTokenRequest, RefreshTokenResponse> useCase
        )
    {
        try
        {
            RefreshTokenResponse response = await useCase.Execute(request);
            
            return Ok(new Response<RefreshTokenResponse>(
                StatusCodes.Status200OK, 
                "Refresh login successful", 
                response
                ));
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new Response<object>(
                StatusCodes.Status401Unauthorized,
                ex.Message
                ));
        }
    }
}
