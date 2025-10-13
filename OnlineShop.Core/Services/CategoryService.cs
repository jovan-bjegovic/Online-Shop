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
            List<Category> categories = _repository.GetAll();
            return categories.Any() ? categories.Max(c => c.Id) + 1 : 1;
        }
        
        public Category CreateCategory(Category newCategory)
        {
            if (string.IsNullOrWhiteSpace(newCategory.Title) || string.IsNullOrWhiteSpace(newCategory.Code))
            {
                throw new ArgumentException("Unique code or title are missing.");
            }

            if (_repository.CodeExists(newCategory.Code))
            {
                throw new InvalidOperationException("Code must be unique.");
            }

            List<Category> categories = _repository.GetAll();

            if (categories.Any(c => c.Code.Equals(newCategory.Code, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Code must be unique.");
            }

            return _repository.CreateCategory(newCategory);
        }

        public Category? UpdateCategory(int id, Category updated)
        {
            if (string.IsNullOrWhiteSpace(updated.Title) || string.IsNullOrWhiteSpace(updated.Code))
            {
                throw new ArgumentException("Unique code or title are missing.");
            }

            if (_repository.CodeExists(updated.Code))
            {
                throw new InvalidOperationException("Code must be unique.");
            }

            return _repository.UpdateCategory(id, updated);
        }
    }
}