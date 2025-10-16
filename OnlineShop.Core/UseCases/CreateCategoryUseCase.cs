using OnlineShop.Core.Helpers;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Requests;
using OnlineShop.Core.UseCases.Responses;

namespace OnlineShop.Core.UseCases;

public class CreateCategoryUseCase(
        ICategoryRepository repository, CategoryHelper categoryHelper
    ) 
    : IUseCase<CreateCategoryRequest, CreateCategoryResponse>
{
    public CreateCategoryResponse Execute(CreateCategoryRequest request)
    {
        if (categoryHelper.CodeExists(request.Code))
        {
            throw new ArgumentException($"A category with code '{request.Code}' already exists.");
        }
        
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Code = request.Code,
            Description = request.Description,
            ParentCategoryId = request.ParentCategoryId
        };

        repository.CreateCategory(category);

        return new CreateCategoryResponse
        {
            Id = category.Id,
            Title = category.Title,
            Code = category.Code,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId
        };
    }
}
