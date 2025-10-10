using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public List<Category> GetAll() => _repository.GetAll();
        public Category? FindCategory(int id) => _repository.FindCategory(id);
        public int CreateNewId(List<Category> list) => _repository.CreateNewId(list);
        public bool RemoveCategory(int id) => _repository.RemoveCategory(id);
        public bool CodeExistsInList(List<Category> list, string code, int excludeId) 
            => _repository.CodeExistsInList(list, code, excludeId);
    }
}