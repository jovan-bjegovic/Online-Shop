using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Controller
{
    [ApiController]
    [Route("admin/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoryController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            List<Category> categories = service.GetAll();

            if (!categories.Any())
            {
                return NotFound(new Response<object>(
                    StatusCodes.Status404NotFound,
                    "Category not found"
                ));
            }

            return Ok(new Response<List<Category>>(
                StatusCodes.Status200OK,
                "Categories retrieved successfully",
                categories
            ));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetCategory(Guid id)
        {
            Category? category = service.FindCategory(id);

            if (category == null)
            {
                return NotFound(new Response<object>(
                    StatusCodes.Status404NotFound,
                    "Category not found"
                ));
            }
            
            return Ok(new Response<Category>(
                StatusCodes.Status200OK,
                "Category retrieved successfully",
                category
            ));
        }

        [HttpPost]
        public IActionResult Create(CategoryDto categoryDto)
        {
            
            Category newCategory = new Category
            {
                Title = categoryDto.Title,
                Code = categoryDto.Code,
                Description = categoryDto.Description,
                ParentCategoryId = categoryDto.ParentCategoryId
            };
            
            Category created = service.CreateCategory(newCategory);
            
            return Created($"/admin/category/{created.Id}",
                new Response<Category>(StatusCodes.Status200OK, "Category created successfully", created));
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, Category category)
        {
            try
            {
                Category? updatedCategory = service.UpdateCategory(id, category);

                if (updatedCategory == null)
                {
                    return NotFound(new Response<object>(
                        StatusCodes.Status404NotFound,
                        $"Category with id '{id}' not found."
                    ));
                }

                return Ok(new Response<Category>(
                    StatusCodes.Status200OK,
                    "Category updated successfully",
                    updatedCategory
                ));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new Response<object>(
                    StatusCodes.Status400BadRequest,
                    ex.Message
                ));
            }
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            bool remove = service.RemoveCategory(id);

            if (!remove)
            {
                return NotFound(new Response<object>(
                    StatusCodes.Status404NotFound,
                    "Category not found and cannot be deleted"
                ));
            }
            
            return Ok(new Response<object>(
                StatusCodes.Status200OK,
                $"Category with id: {id} deleted successfully."
            ));
        }
        
    }
}
