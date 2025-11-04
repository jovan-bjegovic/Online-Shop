namespace OnlineShop.Core.UseCases.Auth.Login;

public class GenerateTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}