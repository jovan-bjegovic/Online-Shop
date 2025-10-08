using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("admin/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _service = new CategoryService();

        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _service.FindCategory(id);

            if (category is not null)
            {
                return Ok(category);
            }

            return NotFound(new Response<object>(
                statusCode: StatusCodes.Status404NotFound, 
                message: $"Category with id {id} not found."
                
            ));
        }


        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            if (string.IsNullOrWhiteSpace(newCategory.Title) || string.IsNullOrWhiteSpace(newCategory.Code))
            {
                return BadRequest(new Response<object>(
                    StatusCodes.Status400BadRequest,
                    "Unique code or title are missing"
                ));
            }

            var allCategories = _service.GetAll();
            if (allCategories.Any(c => c.Code.Equals(newCategory.Code, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest(new Response<object>(
                    StatusCodes.Status400BadRequest,
                    "Code must be unique"
                ));
            }

            var newId = _service.GetMaxId(allCategories) + 1;
            newCategory.Id = newId;

            if (newCategory.ParentCategoryId.HasValue)
            {
                var parent = _service.FindCategory(newCategory.ParentCategoryId.Value);
                if (parent is null)
                {
                    return NotFound(new Response<object>(
                        StatusCodes.Status404NotFound,
                        $"Parent category with id {newCategory.ParentCategoryId.Value} not found."
                    ));
                }
                parent.Subcategories.Add(newCategory);
            }
            else
            {
                allCategories.Add(newCategory);
            }

            return Created(
                $"/admin/categories/{newCategory.Id}",
                new Response<Category>(
                    StatusCodes.Status201Created,
                    "Category created successfully",
                    newCategory
                )
            );

        }


        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Category updated)
        {
            var category = _service.FindCategory(id);
            if (category is null)
            {
                return NotFound(new Response<object>(
                    StatusCodes.Status404NotFound,
                    $"Category with id {id} not found."
                ));
            }
                
            if (string.IsNullOrWhiteSpace(updated.Title) || string.IsNullOrWhiteSpace(updated.Code))
            {
                return BadRequest(new Response<object>(
                    StatusCodes.Status400BadRequest,
                    "Unique code or title are missing"
                ));
            }

            if (_service.CodeExistsInList(_service.GetAll(), updated.Code, id))
            {
                return BadRequest(new Response<object>(
                    StatusCodes.Status400BadRequest,
                    "Code must be unique")
                );
            }

            if (category.ParentCategoryId.HasValue)
            {
                if (!updated.ParentCategoryId.HasValue)
                {
                    return BadRequest(new Response<object>(
                        StatusCodes.Status400BadRequest,
                        "parentCategoryId is required for subcategories"
                    ));
                }
            }
            else
            {
                if (updated.ParentCategoryId.HasValue)
                {
                    return BadRequest(new Response<object>(
                        StatusCodes.Status400BadRequest,
                        "Root categories must not include parentCategoryId"
                    ));
                }
            }

            category.Title = updated.Title;
            category.Code = updated.Code;
            category.Description = updated.Description;
            category.Subcategories = updated.Subcategories;
            category.ParentCategoryId = updated.ParentCategoryId;

            return Ok(category);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var removed = _service.RemoveCategory(id);
            return removed
                ? Ok(new Response<object>(
                    StatusCodes.Status200OK,
                    $"Category with id: {id} deleted successfully."
                ))
                : NotFound(new Response<object>(
                    StatusCodes.Status404NotFound,
                    $"Category with id: {id} not found."
                ));
        }
    }
}
