using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("admin/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoriesController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetCategories() =>
            Ok(_service.GetAll());

        [HttpGet("{id:int}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _service.FindCategory(id);
            return category is not null
                ? Ok(category)
                : NotFound($"Category {id} not found.");
        }

        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            var newId = _service.GetMaxId(_service.GetAll()) + 1;
            newCategory.Id = newId;

            if (newCategory.ParentCategoryId.HasValue)
            {
                var parent = _service.FindCategory(newCategory.ParentCategoryId.Value);
                if (parent is null)
                    return NotFound($"Parent {newCategory.ParentCategoryId} not found.");

                parent.Subcategories.Add(newCategory);
            }
            else
            {
                _service.GetAll().Add(newCategory);
            }

            return Created($"/admin/categories/{newCategory.Id}", newCategory);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Category updated)
        {
            var category = _service.FindCategory(id);
            if (category is null)
                return NotFound($"Category {id} not found.");

            if (!string.IsNullOrEmpty(updated.Title))
                category.Title = updated.Title;

            if (!string.IsNullOrEmpty(updated.Code))
                category.Code = updated.Code;

            if (!string.IsNullOrEmpty(updated.Description))
                category.Description = updated.Description;

            if (updated.ParentCategoryId.HasValue && updated.ParentCategoryId != category.ParentCategoryId)
            {
                if (category.ParentCategoryId.HasValue)
                {
                    var oldParent = _service.FindCategory(category.ParentCategoryId.Value);
                    oldParent?.Subcategories.Remove(category);
                }
                else
                {
                    _service.GetAll().Remove(category);
                }

                var newParent = _service.FindCategory(updated.ParentCategoryId.Value);
                if (newParent is null)
                    return BadRequest($"Parent {updated.ParentCategoryId} not found.");

                newParent.Subcategories.Add(category);
                category.ParentCategoryId = newParent.Id;
            }

            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var removed = _service.RemoveCategory(id);
            return removed
                ? Ok(new { message = $"Category {id} deleted successfully." })
                : NotFound($"Category {id} not found.");
        }
    }
}
