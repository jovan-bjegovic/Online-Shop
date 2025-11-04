namespace OnlineShop.Core.UseCases.Auth.Login;

public class GenerateTokenRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}