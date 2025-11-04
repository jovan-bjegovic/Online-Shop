namespace OnlineShop.Core.UseCases.Auth.Refresh;

public class RefreshTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}