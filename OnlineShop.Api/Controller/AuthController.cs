using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Auth.Login;
using OnlineShop.Core.UseCases.Auth.Refresh;
using LoginRequest = OnlineShop.Core.UseCases.Auth.Login.LoginRequest;

namespace OnlineShop.Controller;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] IUseCase<LoginRequest, LoginResponse> useCase
        )
    {
        try
        {
            LoginResponse response = await useCase.Execute(request);
            
            return Ok(new Response<LoginResponse>(
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
    public async Task<IActionResult> Refresh(
        [FromBody] RefreshRequest request,
        [FromServices] IUseCase<RefreshRequest, LoginResponse> useCase
        )
    {
        try
        {
            LoginResponse response = await useCase.Execute(request);
            
            return Ok(new Response<LoginResponse>(
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
