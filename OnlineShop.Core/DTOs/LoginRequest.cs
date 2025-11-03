namespace OnlineShop.Core.DTOs;

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RefreshRequest
{
    public string RefreshToken { get; set; }
}