using System.Text.Json;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Data.Repositories;

public class JsonCategoryRepository : BaseCategoryRepository, ICategoryRepository
{
    private readonly string filePath;
    private readonly JsonSerializerOptions jsonOptions;

    public JsonCategoryRepository(string filePath)
    {
        this.filePath = filePath;
        jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public List<Category> GetAll()
    {
        string json = File.ReadAllText(filePath);
        List<Category>? categories = JsonSerializer.Deserialize<List<Category>>(json, jsonOptions);
            
        return categories ?? new List<Category>();
    }

    public Category? FindCategory(Guid id)
    {
        List<Category> categories = GetAll();
            
        return FindCategoryRecursive(id, categories);
    }

    public bool RemoveCategory(Guid id)
    {
        List<Category> categories = GetAll();
        bool removed = RemoveCategoryRecursive(id, categories);
        if (removed)
        {
            SaveAll(categories);
        }
            
        return removed;
    }

    private void SaveAll(List<Category> categories)
    {
        string json = JsonSerializer.Serialize(categories, jsonOptions);
        File.WriteAllText(filePath, json);
    }

    public Category CreateCategory(Category category)
    {
        List<Category> categories = GetAll();

        category.Id = Guid.NewGuid();

        if (category.ParentCategoryId.HasValue)
        {
            Category? parent = FindCategoryRecursive(category.ParentCategoryId.Value, categories);
            if (parent == null)
            {
                throw new KeyNotFoundException(
                    $"Parent category with id {category.ParentCategoryId.Value} not found.");
            }

            parent.Subcategories.Add(category);
        }
        else
        {
            categories.Add(category);
        }

        SaveAll(categories);
            
        return category;
    }

    public Category? UpdateCategory(Guid id, Category updated)
    {
        List<Category> categories = GetAll();
        Category? category = FindCategoryRecursive(id, categories);
        if (category == null)
        {
            return null;
        }

        category.Title = updated.Title;
        category.Code = updated.Code;
        category.Description = updated.Description;

        if (category.ParentCategoryId != updated.ParentCategoryId)
        {
            if (category.ParentCategoryId.HasValue)
            {
                Category? oldParent = FindCategoryRecursive(category.ParentCategoryId.Value, categories);
                oldParent?.Subcategories.RemoveAll(c => c.Id == category.Id);
            }
            else
            {
                categories.RemoveAll(c => c.Id == category.Id);
            }

            if (updated.ParentCategoryId.HasValue)
            {
                Category? newParent = FindCategoryRecursive(updated.ParentCategoryId.Value, categories);
                if (newParent == null)
                {
                    throw new KeyNotFoundException(
                        $"Parent category with id {updated.ParentCategoryId.Value} not found.");
                }

                newParent.Subcategories.Add(category);
            }
            else
            {
                categories.Add(category);
            }

            category.ParentCategoryId = updated.ParentCategoryId;
        }

        SaveAll(categories);
        return category;
    }

}