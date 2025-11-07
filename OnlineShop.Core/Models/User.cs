namespace OnlineShop.Core.Models;

public class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Username { get; init; }
    public required string PasswordHash { get; init; }
    public required string Role { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}