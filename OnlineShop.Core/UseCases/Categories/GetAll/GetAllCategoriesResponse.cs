using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Responses;

public class GetAllCategoriesResponse
{
    public List<Category> Categories { get; set; } = new();
}