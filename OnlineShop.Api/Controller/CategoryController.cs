using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Requests;
using OnlineShop.Core.UseCases.Responses;

namespace OnlineShop.Controller;

[ApiController]
[Route("admin/[controller]")]
public class CategoryController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllCategories(
        [FromServices] IUseCase<GetAllCategoriesResponse> getAllCategoriesUseCase
        )
    {
        GetAllCategoriesResponse response = getAllCategoriesUseCase.Execute();

        if (response.Categories.Count == 0)
        {
            return NotFound(new Response<object>(
                StatusCodes.Status404NotFound,
                "Category not found"
            ));
        }

        return Ok(new Response<List<Category>>(
            StatusCodes.Status200OK,
            "Categories retrieved successfully",
            response.Categories
        ));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetCategoryById(
        [FromRoute] Guid id, 
        [FromServices] IUseCase<GetCategoryRequest, GetCategoryResponse> getCategoryByIdUseCase
        )
    {
        GetCategoryResponse response = getCategoryByIdUseCase.Execute(new GetCategoryRequest { Id = id });

        if (response.Category == null)
        {
            return NotFound(new Response<object>(
                StatusCodes.Status404NotFound,
                "Category not found"
            ));
        }
            
        return Ok(new Response<Category>(
            StatusCodes.Status200OK,
            "Category retrieved successfully",
            response.Category
        ));
    }

    [HttpPost]
    public IActionResult Create(
        [FromBody] CreateCategoryRequest request,
        [FromServices] IUseCase<CreateCategoryRequest, CreateCategoryResponse> createCategoryUseCase
        )
    {
        try
        {
            var response = createCategoryUseCase.Execute(request);
            
            return Created(
                $"/admin/category/{response.Id}",
                new Response<CreateCategoryResponse>(
                    StatusCodes.Status201Created,
                    "Category created successfully",
                    response
                )
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new Response<object>(
                StatusCodes.Status400BadRequest,
                ex.Message
            ));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new Response<object>(
                StatusCodes.Status404NotFound,
                ex.Message
            ));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response<Category>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while creating the category."
                ));
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(
        [FromRoute] Guid id,
        [FromBody] UpdateCategoryRequest request,
        [FromServices] IUseCase<UpdateCategoryRequest, UpdateCategoryResponse> updateCategoryUseCase)
    {
        try
        {
            request.Id = id;
            var response = updateCategoryUseCase.Execute(request);
            
            return Ok(new Response<UpdateCategoryResponse>(
                StatusCodes.Status200OK,
                "Category updated successfully",
                response
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
    public IActionResult Delete(
        [FromRoute] Guid id,
        [FromServices] IUseCase<DeleteCategoryRequest, DeleteCategoryResponse> deleteCategoryUseCase)
    {
        var response = deleteCategoryUseCase.Execute(new DeleteCategoryRequest { Id = id });
        
        if (!response.Success)
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