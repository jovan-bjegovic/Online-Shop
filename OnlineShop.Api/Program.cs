using OnlineShop.Core.Interfaces;
using OnlineShop.Data.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using OnlineShop.Core.Helpers;
using OnlineShop.Core.UseCases;
using OnlineShop.Core.UseCases.Requests;
using OnlineShop.Core.UseCases.Responses;
using OnlineShop.Core.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();

var jsonFilePath = Path.Combine(builder.Environment.ContentRootPath, "categories.json");
builder.Services.AddSingleton<ICategoryRepository>(
    new JsonCategoryRepository(jsonFilePath)
);
builder.Services.AddScoped<IUseCase<GetAllCategoriesResponse>, GetAllCategoriesUseCase>();
builder.Services.AddScoped<IUseCase<CreateCategoryRequest, CreateCategoryResponse>, CreateCategoryUseCase>();
builder.Services.AddScoped<IUseCase<UpdateCategoryRequest, UpdateCategoryResponse>, UpdateCategoryUseCase>();
builder.Services.AddScoped<IUseCase<DeleteCategoryRequest, DeleteCategoryResponse>, DeleteCategoryUseCase>();
builder.Services.AddScoped<IUseCase<GetCategoryRequest, GetCategoryResponse>, GetCategoryByIdUseCase>();
builder.Services.AddScoped<CategoryHelper>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();