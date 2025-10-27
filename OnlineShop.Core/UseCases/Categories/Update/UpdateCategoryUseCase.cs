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
    public async Task<UpdateCategoryResponse> Execute(UpdateCategoryRequest request)
    {
        Category? existing = await repository.FindCategory(request.Id);
        
        List<Category> categories = await repository.GetAll();
        
        if (existing == null)
        {
            throw new KeyNotFoundException($"Category with id '{request.Id}' not found.");
        }

        if (CategoryHelper.CodeExists(categories, request.Code, request.Id))
        {
            throw new InvalidOperationException($"Code '{request.Code}' already exists.");
        }
        
        if (request.ParentCategoryId.HasValue && request.ParentCategoryId.Value == request.Id)
        {
            throw new InvalidOperationException("A category cannot have itself as a parent.");
        }
        
        if (request.ParentCategoryId.HasValue)
        {
            bool isParent = CategoryHelper.IsCircularParent(categories, existing.Id, request.ParentCategoryId.Value);
            if (isParent)
            {
                throw new InvalidOperationException("Cannot set this parent. It would create a circular relationship.");
            }
        }

        existing.Title = request.Title;
        existing.Code = request.Code;
        existing.Description = request.Description;
        existing.ParentCategoryId = request.ParentCategoryId;

        await repository.UpdateCategory(existing);
        await unitOfWork.CommitAsync();

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