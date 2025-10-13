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
        
        public Category CreateCategory(Category newCategory)
        {
            if (_repository is not IWritableCategoryRepository writableRepo)
                throw new InvalidOperationException("Repository does not support writing.");

            if (string.IsNullOrWhiteSpace(newCategory.Title) || string.IsNullOrWhiteSpace(newCategory.Code))
                throw new ArgumentException("Unique code or title are missing.");

            var categories = _repository.GetAll();

            if (categories.Any(c => c.Code.Equals(newCategory.Code, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Code must be unique.");

            return writableRepo.CreateCategory(newCategory);
        }

        public Category? UpdateCategory(int id, Category updated)
        {
            if (_repository is not IWritableCategoryRepository writableRepo)
                throw new InvalidOperationException("Repository does not support writing.");

            if (string.IsNullOrWhiteSpace(updated.Title) || string.IsNullOrWhiteSpace(updated.Code))
                throw new ArgumentException("Unique code or title are missing.");

            if (CodeExists(updated.Code, id))
                throw new InvalidOperationException("Code must be unique.");

            return writableRepo.UpdateCategory(id, updated);
        }
    }
}