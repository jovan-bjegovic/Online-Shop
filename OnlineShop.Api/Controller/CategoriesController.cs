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
            if (category is not null)
            {
                return Ok(new Response<Category>(
                    StatusCodes.Status200OK,
                    "Category retrieved successfully",
                    category
                ));
            }

            return NotFound(new Response<object>(
                StatusCodes.Status404NotFound, 
                $"Category with id {id} not found."
            ));
        }

        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            try
            {
                Category created = _service.CreateCategory(newCategory);
                return Created($"/admin/categories/{created.Id}",
                    new Response<Category>(StatusCodes.Status200OK, "Category created successfully", created));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new Response<object>(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new Response<object>(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new Response<object>(StatusCodes.Status404NotFound, ex.Message));
            }
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, Category updated)
        {
            try
            {
                Category category = _service.UpdateCategory(id, updated);
                if (category == null)
                    return NotFound(new Response<object>(StatusCodes.Status404NotFound, $"Category {id} not found."));

                return Ok(new Response<Category>(StatusCodes.Status200OK, "Category updated successfully", category));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new Response<object>(StatusCodes.Status400BadRequest, ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new Response<object>(StatusCodes.Status400BadRequest, ex.Message));
            }
        }


        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            bool removed = _service.RemoveCategory(id);
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
