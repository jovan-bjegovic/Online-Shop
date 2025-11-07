using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.UseCases.Upload;
using OnlineShop.Models;

namespace OnlineShop.Controller;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("admin/[controller]")]
public class ImagesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadImage(
        [FromForm] IFormFile image,
        [FromServices] IUseCase<UploadImageRequest, UploadImageResponse> useCase)
    {
        try
        {
            UploadImageResponse response = await useCase.Execute(
                new UploadImageRequest
                {
                    Image = image
                }
                );
            return Created($"/admin/images/{response.Id}",
                new Response<UploadImageResponse>(
                    StatusCodes.Status201Created,
                    "Image uploaded successfully",
                    response
                ));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new Response<object>(
                StatusCodes.Status400BadRequest, ex.Message));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response<object>(
                    StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred while uploading image."
                ));
        }
    }
}