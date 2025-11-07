using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Services;
using OnlineShop.Core.UseCases.Auth.Login;
using OnlineShop.Core.UseCases.Auth.Refresh;
using OnlineShop.Core.UseCases.Categories.Create;
using OnlineShop.Core.UseCases.Categories.Delete;
using OnlineShop.Core.UseCases.Categories.Get;
using OnlineShop.Core.UseCases.Categories.GetAll;
using OnlineShop.Core.UseCases.Categories.Update;
using OnlineShop.Core.UseCases.Products.Create;
using OnlineShop.Core.UseCases.Products.Delete;
using OnlineShop.Core.UseCases.Products.Disable;
using OnlineShop.Core.UseCases.Products.Enable;
using OnlineShop.Core.UseCases.Products.Get;
using OnlineShop.Core.UseCases.Products.GetAll;
using OnlineShop.Core.UseCases.Products.SetProductImage;
using OnlineShop.Core.UseCases.Products.Update;
using OnlineShop.Core.UseCases.Products.UploadImage;

namespace OnlineShop.Core;

public static class DIConfiguration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUseCase<GetAllCategoriesResponse>, GetAllCategoriesUseCase>();
        services.AddScoped<IUseCase<CreateCategoryRequest, CreateCategoryResponse>, CreateCategoryUseCase>();
        services.AddScoped<IUseCase<UpdateCategoryRequest, UpdateCategoryResponse>, UpdateCategoryUseCase>();
        services.AddScoped<IUseCase<DeleteCategoryRequest, DeleteCategoryResponse>, DeleteCategoryUseCase>();
        services.AddScoped<IUseCase<GetCategoryRequest, GetCategoryResponse>, GetCategoryUseCase>();

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IUseCase<GenerateTokenRequest, GenerateTokenResponse>, GenerateTokenUseCase>();
        services.AddScoped<IUseCase<RefreshTokenRequest,  RefreshTokenResponse>, RefreshTokenUseCase>();
        
        services.AddScoped<IUseCase<GetAllProductsRequest, GetAllProductsResponse>, GetAllProductsUseCase>();
        services.AddScoped<IUseCase<GetProductRequest, GetProductResponse>, GetProductUseCase>();
        services.AddScoped<IUseCase<CreateProductRequest,  CreateProductResponse>, CreateProductUseCase>();
        services.AddScoped<IUseCase<UpdateProductRequest,   UpdateProductResponse>, UpdateProductUseCase>();
        services.AddScoped<IUseCase<DeleteProductsRequest, DeleteProductsResponse>, DeleteProductsUseCase>();
        services.AddScoped<IUseCase<EnableProductsRequest, EnableProductsResponse>, EnableProductsUseCase>();
        services.AddScoped<IUseCase<DisableProductsRequest, DisableProductsResponse>,  DisableProductsUseCase>();
        services.AddScoped<IUseCase<UploadProductImageRequest, UploadProductImageResponse>, UploadProductImageUseCase>();
        services.AddScoped<IUseCase<SetProductImageRequest, SetProductImageResponse>, SetProductImageUseCase>();

        return services;
    }
}