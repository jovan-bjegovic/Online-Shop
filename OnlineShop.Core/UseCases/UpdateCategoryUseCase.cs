using OnlineShop.Core.Helpers;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Requests;
using OnlineShop.Core.UseCases.Responses;

namespace OnlineShop.Core.UseCases;

public class UpdateCategoryUseCase(
        ICategoryRepository repository,  
        CategoryHelper categoryHelper
    ) 
    : IUseCase<UpdateCategoryRequest, UpdateCategoryResponse>
{
    public UpdateCategoryResponse Execute(UpdateCategoryRequest request)
    {
        var existing = repository.FindCategory(request.Id);
        if (existing == null)
            throw new KeyNotFoundException($"Category with id '{request.Id}' not found.");

        if (categoryHelper.CodeExists(request.Code, request.Id))
            throw new InvalidOperationException($"Code '{request.Code}' already exists.");

        existing.Title = request.Title;
        existing.Code = request.Code;
        existing.Description = request.Description;
        existing.ParentCategoryId = request.ParentCategoryId;

        repository.UpdateCategory(request.Id, existing);

        return new UpdateCategoryResponse
        {
            Id = existing.Id,
            Title = existing.Title,
            Code = existing.Code,
            Description = existing.Description,
            ParentCategoryId = existing.ParentCategoryId
        };
    }
}
