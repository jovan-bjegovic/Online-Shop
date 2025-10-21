using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.Get;

public class GetCategoryUseCase(IUnitOfWork unitOfWork)
    : IUseCase<GetCategoryRequest, GetCategoryResponse>
{
    public GetCategoryResponse Execute(GetCategoryRequest request)
    {
        Category? category = unitOfWork.Categories.FindCategory(request.Id);

        return new GetCategoryResponse
        {
            Category = category
        };
    }
}