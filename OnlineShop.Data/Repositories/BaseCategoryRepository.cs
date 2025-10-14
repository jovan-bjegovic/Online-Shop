using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories
{
    public abstract class BaseCategoryRepository
    {
        private void TraverseCategories(List<Category> categories, Action<Category> action)
        {
            foreach (Category c in categories)
            {
                action(c);
                if (c.Subcategories is { Count: > 0 })
                    TraverseCategories(c.Subcategories, action);
            }
        }

        protected Category? FindCategoryRecursive(Guid id, List<Category> list)
        {
            Category? found = null;
            TraverseCategories(list, c =>
            {
                if (found == null && c.Id == id)
                    found = c;
            });
            
            return found;
        }

        protected bool RemoveCategoryRecursive(Guid id, List<Category> list)
        {
            Category? category = list.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                list.Remove(category);
                
                return true;
            }

            foreach (Category cat in list)
            {
                if (cat.Subcategories is { Count: > 0 } &&
                    RemoveCategoryRecursive(id, cat.Subcategories))
                {
                    return true;
                }
            }

            return false;
        }

    }
}