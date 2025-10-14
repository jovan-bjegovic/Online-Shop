using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repository;

        public CategoryService(ICategoryRepository repository)
        {
            this.repository = repository;
        }

        public List<Category> GetAll()
        {
            return repository.GetAll();
        }

        public Category? FindCategory(Guid id)
        {
            return repository.FindCategory(id);
        }
        
        public bool RemoveCategory(Guid id)
        {
            return repository.RemoveCategory(id);
        }
        
        public Category CreateCategory(Category newCategory)
        {
            return repository.CreateCategory(newCategory);
        }

        public Category? UpdateCategory(Guid id, Category updated)
        {
            Category? existing = repository.FindCategory(id);
            if (existing == null)
            {
                return null;
            }

            if (repository.CodeExists(updated.Code) && !string.Equals(existing.Code, updated.Code, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"A category with code '{updated.Code}' already exists.");
            }

            return repository.UpdateCategory(id, updated);
        }

    }
}