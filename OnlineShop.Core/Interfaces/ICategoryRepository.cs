using OnlineShop.Core.Models;

namespace OnlineShop.Core.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category? FindCategory(int id);
        int CreateNewId(List<Category> list);
        bool RemoveCategory(int id);
        bool CodeExistsInList(List<Category> list, string code, int excludeId);
    }
}