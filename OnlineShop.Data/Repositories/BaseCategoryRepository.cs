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

        protected Category? FindCategoryRecursive(int id, List<Category> list)
        {
            Category? found = null;
            TraverseCategories(list, c =>
            {
                if (found == null && c.Id == id)
                    found = c;
            });
            return found;
        }

        protected bool RemoveCategoryRecursive(int id, List<Category> list)
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
                    return true;
            }

            return false;
        }

        protected int GetMaxIdRecursive(List<Category> categories)
        {
            int maxId = 0;
            TraverseCategories(categories, c =>
            {
                if (c.Id > maxId)
                    maxId = c.Id;
            });
            return maxId;
        }
        
        protected bool CodeExistsRecursive(List<Category> categories, string code, int excludeId = -1)
        {
            bool exists = false;

            TraverseCategories(categories, c =>
            {
                if (exists) return;
                if (c.Id != excludeId && string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase))
                    exists = true;
            });

            return exists;
        }
        
        protected bool CodeExistsInList(List<Category> categories, string code, int excludeId = -1)
        {
            return CodeExistsRecursive(categories, code, excludeId);
        }
    }
}