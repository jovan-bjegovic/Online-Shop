using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Requests;
using OnlineShop.Core.UseCases.Responses;

namespace OnlineShop.Core.UseCases;

public class GetCategoryUseCase : IUseCase<GetCategoryRequest, GetCategoryResponse>
{
    private readonly ICategoryRepository _repository;

    public GetCategoryUseCase(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public GetCategoryResponse Execute(GetCategoryRequest request)
    {
        Category? category = _repository.FindCategory(request.Id);
        return new GetCategoryResponse { Category = category };
    }
}
