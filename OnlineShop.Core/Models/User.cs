namespace OnlineShop.Core.Models;

public class User : BaseEntity
{
    public required string Username { get; init; }
    public required string PasswordHash { get; init; } 
    public required string Role { get; init; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}
