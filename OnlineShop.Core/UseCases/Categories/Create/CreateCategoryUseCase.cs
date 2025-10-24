using OnlineShop.Core.Helpers;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.Create;

public class CreateCategoryUseCase(
    ICategoryRepository repository,
    IUnitOfWork unitOfWork, 
    CategoryHelper categoryHelper)
    : IUseCase<CreateCategoryRequest, CreateCategoryResponse>
{
    public async Task<CreateCategoryResponse> Execute(CreateCategoryRequest request)
    {
        if (await categoryHelper.CodeExists(request.Code))
        {
            throw new ArgumentException($"A category with code '{request.Code}' already exists.");
        }
        
        Category category = new Category
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Code = request.Code,
            Description = request.Description,
            ParentCategoryId = request.ParentCategoryId
        };

        await repository.CreateCategory(category);
        await unitOfWork.CommitAsync();

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