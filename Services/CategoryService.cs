using OnlineShop.Models;

namespace OnlineShop.Services
{
    public class CategoryService
    {
        private readonly List<Category> _categories;

        public CategoryService()
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

        public List<Category> GetAll() => _categories;

        public Category? FindCategory(int id, List<Category>? list = null)
        {
            list ??= _categories;
            foreach (var cat in list)
            {
                if (cat.Id == id)
                    return cat;

                var found = FindCategory(id, cat.Subcategories);
                if (found != null)
                    return found;
            }

            return null;
        }

        public int GetMaxId(List<Category> list)
        {
            if (!list.Any()) return 0;

            int max = list.Max(c => c.Id);

            foreach (var cat in list)
            {
                int subMax = GetMaxId(cat.Subcategories);
                if (subMax > max)
                    max = subMax;
            }

            return max;
        }

        public bool RemoveCategory(int id, List<Category>? list = null)
        {
            list ??= _categories;

            var category = list.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                list.Remove(category);
                return true;
            }

            foreach (var cat in list)
            {
                if (RemoveCategory(id, cat.Subcategories))
                    return true;
            }

            return false;
        }
    }
}
