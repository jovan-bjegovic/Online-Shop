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

        public Category? FindCategory(Guid id)
        {
            return _repository.FindCategory(id);
        }
        
        public bool RemoveCategory(Guid id)
        {
            return _repository.RemoveCategory(id);
        }
        
        public Category CreateCategory(Category newCategory)
        {
            return _repository.CreateCategory(newCategory);
        }

        public Category? UpdateCategory(Guid id, Category updated)
        {
            return _repository.UpdateCategory(id, updated);
        }
    }
}