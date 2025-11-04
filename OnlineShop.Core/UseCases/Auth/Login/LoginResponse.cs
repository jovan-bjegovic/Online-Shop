namespace OnlineShop.Core.UseCases.Auth.Login;

public class LoginResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}