namespace OnlineShop.Core.Models;

public class Response<T>(int statusCode, string message, T? data = default)
{
    public int StatusCode { get; set; } = statusCode;
    public string Message { get; set; } = message;
    public T? Data { get; set; } = data;
}