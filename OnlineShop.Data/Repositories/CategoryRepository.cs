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
    }
}
