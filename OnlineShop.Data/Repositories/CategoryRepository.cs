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
                    Id = Guid.Parse("a7c1f2c8-59b8-4a9a-b046-7d9db27dfe59"),
                    Title = "Laptops",
                    Code = "LAP",
                    Description = "All types of laptops",
                    Subcategories = new List<Category>
                    {
                        new Category
                        {
                            Id = Guid.Parse("b2e77a4c-fb52-4d9d-b0d0-91a2c367e7de"),
                            Title = "Office",
                            Code = "OFF",
                            Description = "Laptops for office use",
                            ParentCategoryId = Guid.Parse("a7c1f2c8-59b8-4a9a-b046-7d9db27dfe59")
                        },
                        new Category
                        {
                            Id = Guid.Parse("c4a7d1f6-226b-47b2-8b65-3f6d56d6c241"),
                            Title = "Gaming",
                            Code = "GAM",
                            Description = "Gaming laptops with high performance",
                            ParentCategoryId = Guid.Parse("a7c1f2c8-59b8-4a9a-b046-7d9db27dfe59")
                        }
                    }
                }
            };
        }

        public List<Category> GetAll()
        {
            return _categories;
        }

        public Category? FindCategory(Guid id)
        {
            return FindCategoryRecursive(id, _categories);
        }

        public bool RemoveCategory(Guid id)
        {
            return RemoveCategoryRecursive(id, _categories);
        }
        
        public Category CreateCategory(Category category)
        {
            category.Id = Guid.NewGuid();

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

        public Category? UpdateCategory(Guid id, Category updated)
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
        
        public bool CodeExists(string code)
        {
            return CodeExistsInList(GetAll(), code);
        }
    }
}
