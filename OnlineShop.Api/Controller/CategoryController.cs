using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.DTOs;

namespace OnlineShop.Controller;

[ApiController]
[Route("admin/[controller]")]
public class CategoryController(
        IMapper mapper
    ) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllCategories(
        [FromServices] IUseCase<List<Category>> getAllCategoriesUseCase
        )
    {
        List<Category> categories = getAllCategoriesUseCase.Execute();

        if (categories.Count == 0)
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
    public IActionResult GetCategory(
        [FromRoute] Guid id, 
        [FromServices] IUseCase<Guid, Category?> getCategoryByIdUseCase
        )
    {
        Category? category = getCategoryByIdUseCase.Execute(id);

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
    public IActionResult Create(
        [FromBody] CategoryDto categoryDto,
        [FromServices] IUseCase<Category, Category> createCategoryUseCase
        )
    {
        try
        {
            Category category = mapper.Map<Category>(categoryDto);
            
            Category created = createCategoryUseCase.Execute(category);
            
            return Created(
                $"/admin/category/{created.Id}",
                new Response<Category>(
                    StatusCodes.Status201Created,
                    "Category created successfully",
                    created
                )
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new Response<Category>(
                StatusCodes.Status400BadRequest,
                ex.Message
            ));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new Response<Category>(
                StatusCodes.Status404NotFound,
                ex.Message
            ));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response<Category>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while creating the category."
                ));
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update([FromRoute] Guid id,
        [FromBody] CategoryDto categoryDto,
        [FromServices] IUseCase<(Guid, Category), Category?> updateCategoryUseCase
        )
    {
        try
        {
            var updatedEntity = mapper.Map<Category>(categoryDto);
            
            Category? updatedCategory = updateCategoryUseCase.Execute((id, updatedEntity));

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
    public IActionResult Delete([FromRoute] Guid id,
        [FromServices] IUseCase<Guid, bool> deleteCategoryUseCase
        )
    {
        bool success = deleteCategoryUseCase.Execute(id);

        if (!success)
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