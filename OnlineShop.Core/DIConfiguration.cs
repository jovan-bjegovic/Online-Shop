using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Core.Helpers;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.UseCases;
using OnlineShop.Core.UseCases.Requests;
using OnlineShop.Core.UseCases.Responses;

namespace OnlineShop.Core
{
    public static class DIConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUseCase<GetAllCategoriesResponse>, GetAllCategoriesUseCase>();
            services.AddScoped<IUseCase<CreateCategoryRequest, CreateCategoryResponse>, CreateCategoryUseCase>();
            services.AddScoped<IUseCase<UpdateCategoryRequest, UpdateCategoryResponse>, UpdateCategoryUseCase>();
            services.AddScoped<IUseCase<DeleteCategoryRequest, DeleteCategoryResponse>, DeleteCategoryUseCase>();
            services.AddScoped<IUseCase<GetCategoryRequest, GetCategoryResponse>, GetCategoryUseCase>();

            services.AddScoped<CategoryHelper>();

            return services;
        }
    }
}