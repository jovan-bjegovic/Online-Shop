using System.Text.Json;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories
{
    public class JsonCategoryRepository : BaseCategoryRepository, IWritableCategoryRepository
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions;

        public JsonCategoryRepository(string filePath)
        {
            _filePath = filePath;
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public List<Category> GetAll()
        {
            string json = File.ReadAllText(_filePath);
            var categories = JsonSerializer.Deserialize<List<Category>>(json, _jsonOptions);
            return categories ?? new List<Category>();
        }

        public Category? FindCategory(int id)
        {
            var categories = GetAll();
            return FindCategoryRecursive(id, categories);
        }

        public bool RemoveCategory(int id)
        {
            var categories = GetAll();
            var removed = RemoveCategoryRecursive(id, categories);
            if (removed)
            {
                SaveAll(categories);
            }
            return removed;
        }

        public void SaveAll(List<Category> categories)
        {
            string json = JsonSerializer.Serialize(categories, _jsonOptions);
            File.WriteAllText(_filePath, json);
        }

        public Category CreateCategory(Category category)
        {
            var categories = GetAll();
            int newId = categories.Any() ? GetMaxIdRecursive(categories) + 1 : 1;
            category.Id = newId;

            if (category.ParentCategoryId.HasValue)
            {
                var parent = FindCategoryRecursive(category.ParentCategoryId.Value, categories);
                if (parent == null)
                    throw new KeyNotFoundException($"Parent category with id {category.ParentCategoryId.Value} not found.");

                parent.Subcategories.Add(category);
            }
            else
            {
                categories.Add(category);
            }

            SaveAll(categories);
            return category;
        }

        public Category? UpdateCategory(int id, Category updated)
        {
            var categories = GetAll();
            var category = FindCategoryRecursive(id, categories);
            if (category == null)
                return null;

            category.Title = updated.Title;
            category.Code = updated.Code;
            category.Description = updated.Description;
            category.Subcategories = updated.Subcategories ?? new List<Category>();
            category.ParentCategoryId = updated.ParentCategoryId;

            SaveAll(categories);
            return category;
        }
    }
}
