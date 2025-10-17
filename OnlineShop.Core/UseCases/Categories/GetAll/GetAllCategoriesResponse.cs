using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.GetAll;

public class GetAllCategoriesResponse
{
    public List<Category> Categories { get; set; } = new();
}