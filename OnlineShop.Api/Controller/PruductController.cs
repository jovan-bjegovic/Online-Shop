using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Products.Create;
using OnlineShop.Core.UseCases.Products.Delete;
using OnlineShop.Core.UseCases.Products.Disable;
using OnlineShop.Core.UseCases.Products.Enable;
using OnlineShop.Core.UseCases.Products.Get;
using OnlineShop.Core.UseCases.Products.GetAll;
using OnlineShop.Core.UseCases.Products.SetProductImage;
using OnlineShop.Core.UseCases.Products.Update;
using OnlineShop.Core.UseCases.Products.UploadImage;
using OnlineShop.Models;

namespace OnlineShop.Controller;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("admin/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllProducts(
        [FromServices] IUseCase<GetAllProductsRequest, GetAllProductsResponse> useCase,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var request = new GetAllProductsRequest
            {
                Page = page,
                PageSize = pageSize
            };

            GetAllProductsResponse response = await useCase.Execute(request);

            if (response.Products.Count == 0)
            {
                return Ok(new Response<GetAllProductsResponse>(
                    StatusCodes.Status200OK,
                    "No products available",
                    response
                ));
            }

            return Ok(new Response<List<Product>>(
                StatusCodes.Status200OK,
                "Products retrieved successfully",
                response.Products
            ));
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
                new Response<object>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while retrieving products."
                ));
        }
    }


    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProduct(
        [FromRoute] Guid id,
        [FromServices] IUseCase<GetProductRequest, GetProductResponse> useCase)
    {
        try
        {
            var response = await useCase.Execute(new GetProductRequest { Id = id });

            return Ok(new Response<Product>(
                StatusCodes.Status200OK,
                "Product retrieved successfully",
                response.Product
            ));
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
                new Response<object>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while retrieving the product."
                ));
        }
    }


    [HttpPost]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductRequest request,
        [FromServices] IUseCase<CreateProductRequest, CreateProductResponse> useCase)
    {
        try
        {
            CreateProductResponse response = await useCase.Execute(request);

            return Created(
                $"/admin/products/{response.Id}",
                new Response<CreateProductResponse>(
                    StatusCodes.Status201Created,
                    "Product created successfully",
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
            return BadRequest(new Response<object>(
                StatusCodes.Status404NotFound,
                ex.Message
            ));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response<object>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while creating the product."
                ));
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] Guid id,
        [FromBody] UpdateProductRequest request,
        [FromServices] IUseCase<UpdateProductRequest, UpdateProductResponse> useCase)
    {
        try
        {
            request.Id = id;
            UpdateProductResponse response = await useCase.Execute(request);

            return Ok(new Response<UpdateProductResponse>(
                StatusCodes.Status200OK,
                "Product updated successfully",
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
                new Response<object>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while updating the product."
                ));
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProducts(
        [FromBody] DeleteProductsRequest request,
        [FromServices] IUseCase<DeleteProductsRequest, DeleteProductsResponse> useCase)
    {
        try
        {
            DeleteProductsResponse response = await useCase.Execute(request);

            if (!response.Success)
            {
                return NotFound(new Response<object>(
                    StatusCodes.Status404NotFound,
                    "Some or all products not found and cannot be deleted"
                ));
            }

            return Ok(new Response<object>(
                StatusCodes.Status200OK,
                "Products deleted successfully"
            ));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new Response<object>(
                StatusCodes.Status400BadRequest,
                ex.Message
            ));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response<object>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while deleting products."
                ));
        }
    }

    [HttpPatch("enable")]
    public async Task<IActionResult> EnableProducts(
        [FromBody] EnableProductsRequest request,
        [FromServices] IUseCase<EnableProductsRequest, EnableProductsResponse> useCase)
    {
        try
        {
            EnableProductsResponse response = await useCase.Execute(request);

            return Ok(new Response<EnableProductsResponse>(
                StatusCodes.Status200OK,
                "Products enabled successfully",
                response
            ));
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
                new Response<object>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while enabling products."
                ));
        }
    }

    [HttpPatch("disable")]
    public async Task<IActionResult> DisableProducts(
        [FromBody] DisableProductsRequest request,
        [FromServices] IUseCase<DisableProductsRequest, DisableProductsResponse> useCase)
    {
        try
        {
            DisableProductsResponse response = await useCase.Execute(request);

            return Ok(new Response<DisableProductsResponse>(
                StatusCodes.Status200OK,
                "Products disabled successfully",
                response
            ));
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
                new Response<object>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while disabling products."
                ));
        }
    }
    
    [HttpPost("{id:guid}/image")]
    public async Task<IActionResult> UploadImage(
        [FromRoute] Guid id,
        [FromForm] IFormFile file,
        [FromServices] IUseCase<UploadProductImageRequest, UploadProductImageResponse> uploadUseCase,
        [FromServices] IUseCase<SetProductImageRequest, SetProductImageResponse> setImageUseCase)
    {
        try
        {
            UploadProductImageResponse uploadResponse = await uploadUseCase.Execute(
                new UploadProductImageRequest { Id = id, File = file }
            );

            SetProductImageResponse setImageResponse = await setImageUseCase.Execute(
                new SetProductImageRequest { Id = id, Image = uploadResponse.FilePath }
            );

            return Ok(new Response<SetProductImageResponse>(
                StatusCodes.Status200OK,
                "Image uploaded and set on product successfully",
                setImageResponse
            ));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new Response<object>(
                StatusCodes.Status400BadRequest,
                ex.Message
            ));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response<object>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while uploading the image."
                ));
        }
    }
}