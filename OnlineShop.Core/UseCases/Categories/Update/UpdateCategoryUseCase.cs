using OnlineShop.Core.Helpers;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.Update;

public class UpdateCategoryUseCase(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork, 
    CategoryHelper categoryHelper)
    : IUseCase<UpdateCategoryRequest, UpdateCategoryResponse>
{
    public UpdateCategoryResponse Execute(UpdateCategoryRequest request)
    {
        Category? existing = repository.FindCategory(request.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Category with id '{request.Id}' not found.");
        }

        if (categoryHelper.CodeExists(request.Code, request.Id))
        {
            throw new InvalidOperationException($"Code '{request.Code}' already exists.");
        }

        existing.Title = request.Title;
        existing.Code = request.Code;
        existing.Description = request.Description;
        existing.ParentCategoryId = request.ParentCategoryId;

        repository.UpdateCategory(existing);
        unitOfWork.CommitAsync().GetAwaiter().GetResult();

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