using OnlineShop.Core.Models;
using OnlineShop.Core.Interfaces;

namespace OnlineShop.Data.Repositories
{
    public class CategoryRepository : BaseCategoryRepository, ICategoryRepository
    {
        private readonly List<Category> _categories;

        public CategoryRepository()
        {
            _categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Title = "Laptops",
                    Code = "LAP",
                    Description = "All types of laptops",
                    Subcategories = new List<Category>
                    {
                        new Category
                        {
                            Id = 2,
                            Title = "Office",
                            Code = "OFF",
                            Description = "Laptops for office use",
                            ParentCategoryId = 1
                        },
                        new Category
                        {
                            Id = 3,
                            Title = "Gaming",
                            Code = "GAM",
                            Description = "Gaming laptops with high performance",
                            ParentCategoryId = 1
                        }
                    }
                }
            };
        }

        public List<Category> GetAll()
        {
            return _categories;
        }

        public Category? FindCategory(int id)
        {
            return FindCategoryRecursive(id, _categories);
        }

        public bool RemoveCategory(int id)
        {
            return RemoveCategoryRecursive(id, _categories);
        }
        public Category CreateCategory(Category category)
        {
            int newId = _categories.Any() ? GetMaxIdRecursive(_categories) + 1 : 1;
            category.Id = newId;

            if (category.ParentCategoryId.HasValue)
            {
                Category? parent = FindCategoryRecursive(category.ParentCategoryId.Value, _categories);
                if (parent == null)
                {
                    throw new KeyNotFoundException(
                        $"Parent category with id {category.ParentCategoryId.Value} not found.");
                }

                parent.Subcategories.Add(category);
            }
            else
            {
                _categories.Add(category);
            }

            return category;
        }

        public Category? UpdateCategory(int id, Category updated)
        {
            Category? category = FindCategoryRecursive(id, _categories);
            if (category == null)
            {
                return null;
            }

            category.Title = updated.Title;
            category.Code = updated.Code;
            category.Description = updated.Description;
            category.Subcategories = updated.Subcategories;
            category.ParentCategoryId = updated.ParentCategoryId;

            return category;
        }
        
        
        public bool CodeExists(string code, int excludeId = -1)
        {
            List<Category> categories = GetAll();
            return CodeExistsRecursive(categories, code, excludeId);
        }
        
    }
}
