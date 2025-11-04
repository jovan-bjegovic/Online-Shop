namespace OnlineShop.Core.Options;

public class JwtOptions
{
    public string Secret { get; set; } = string.Empty;
    public int AccessTokenExpirationMinutes { get; set; } = 15;
    public int RefreshTokenExpirationDays { get; set; } = 7;
}