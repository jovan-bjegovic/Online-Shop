using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.DTOs;

namespace OnlineShop.Controller;

[ApiController]
[Route("admin/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService service;
    private readonly IMapper mapper;

    public CategoryController(ICategoryService service,  IMapper mapper)
    {
        this.service = service;
        this.mapper = mapper;
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
    public IActionResult Create(CategoryDto? categoryDto)
    {
        try
        {
            Category category = mapper.Map<Category>(categoryDto);
            
            Category? created = service.CreateCategory(category);
            
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
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response<Category>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while creating the category."
                ));
        }
    }


    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, CategoryDto categoryDto)
    {
        try
        {
            var updatedEntity = mapper.Map<Category>(categoryDto);
            
            Category? updatedCategory = service.UpdateCategory(id, updatedEntity);

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