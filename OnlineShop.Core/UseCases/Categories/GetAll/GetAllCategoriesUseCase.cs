using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;

namespace OnlineShop.Core.UseCases.Categories.GetAll;

public class GetAllCategoriesUseCase(IUnitOfWork unitOfWork)
    : IUseCase<GetAllCategoriesResponse>
{
    public GetAllCategoriesResponse Execute()
    {
        List<Category> categories = unitOfWork.Categories.GetAll();

        return new GetAllCategoriesResponse
        {
            Categories = categories
        };
    }
}
