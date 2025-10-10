using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces;

public interface ICategoryService
{
    List<Category> GetAll();
    Category? FindCategory(int id);
    bool RemoveCategory(int id);
    int CreateNewId();
    bool CodeExists(string code, int excludeId);
}