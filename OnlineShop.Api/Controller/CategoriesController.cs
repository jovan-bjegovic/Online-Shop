using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Controller
{
    [ApiController]
    [Route("admin/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            List<Category> categories = _service.GetAll();
            return Ok(new Response<List<Category>>(
                StatusCodes.Status200OK,
                "Categories retrieved successfully",
                categories
            ));
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetCategoryById(Guid id)
        {
            Category? category = _service.FindCategory(id);
            return Ok(new Response<Category>(
                StatusCodes.Status200OK,
                "Category retrieved successfully",
                category
            ));
        }

        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            Category created = _service.CreateCategory(newCategory);
            
            return Created($"/admin/categories/{created.Id}",
                new Response<Category>(StatusCodes.Status200OK, "Category created successfully", created));
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, Category updated)
        {
            Category? category = _service.UpdateCategory(id, updated);
            
            return Ok(new Response<Category>(StatusCodes.Status200OK, "Category updated successfully", category));
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _service.RemoveCategory(id);

            return Ok(new Response<object>(
                StatusCodes.Status200OK,
                $"Category with id: {id} deleted successfully."
            ));
        }
    }
}
