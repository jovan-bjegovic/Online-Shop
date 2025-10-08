namespace OnlineShop.Models;

public class Category
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Code { get; set; } = "";
    public string Description { get; set; } = "";

    public int? ParentCategoryId { get; set; }

    public List<Category> Subcategories { get; set; } = new();
}

public class Response<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public Response(int statusCode, string message,  T? data = default)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }
}


