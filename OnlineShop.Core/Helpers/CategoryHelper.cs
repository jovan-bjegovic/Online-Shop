using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.Helpers;

public class CategoryHelper
{
    public static bool CodeExists(List<Category> categories, string code, Guid? excludeId = null)
    {
        foreach (Category category in categories)
        {
            
            if (excludeId.HasValue && category.Id == excludeId.Value)
            {
                continue;
            }
            
            if (string.Equals(category.Code, code, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (CodeExists(category.Subcategories, code, excludeId))
            {
                return true;
            }
        }
        
        return false;
    }
    
    public static bool IsCircularParent(List<Category> categories, Guid categoryId, Guid newParentId)
    {
        
        Category? parent = categories.FirstOrDefault(c => c.Id == newParentId);
        while (parent != null)
        {
            if (parent.Id == categoryId)
            {
                return true;
            }
            if (!parent.ParentCategoryId.HasValue)
            {
                break;
            }
            parent = categories.FirstOrDefault(c => c.Id == parent.ParentCategoryId.Value);
        }
        return false;
    }

}