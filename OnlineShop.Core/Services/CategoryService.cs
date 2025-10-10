using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public List<Category> GetAll()
        {
            return _repository.GetAll();
        }

        public Category? FindCategory(int id)
        {
            return _repository.FindCategory(id);
        }
        
        public bool RemoveCategory(int id)
        {
            return _repository.RemoveCategory(id);
        }
        
        public int CreateNewId()
        {
            var categories = _repository.GetAll();
            return categories.Any() ? categories.Max(c => c.Id) + 1 : 1;
        }

        public bool CodeExists(string code, int excludeId)
        {
            var categories = _repository.GetAll();
            return CodeExistsRecursive(categories, code, excludeId);
        }

        private bool CodeExistsRecursive(List<Category> categories, string code, int excludeId)
        {
            foreach (var c in categories)
            {
                if (c.Id != excludeId && string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase))
                    return true;

                if (c.Subcategories is { Count: > 0 } &&
                    CodeExistsRecursive(c.Subcategories, code, excludeId))
                    return true;
            }

            return false;
        }
    }
}