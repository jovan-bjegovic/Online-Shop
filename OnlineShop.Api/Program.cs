using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Services;
using OnlineShop.Data.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases;
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
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IUseCase<List<Category>>, GetAllCategoriesUseCase>();
builder.Services.AddScoped<IUseCase<Category, Category>, CreateCategoryUseCase>();
builder.Services.AddScoped<IUseCase<(Guid, Category), Category?>, UpdateCategoryUseCase>();
builder.Services.AddScoped<IUseCase<Guid, bool>, DeleteCategoryUseCase>();
builder.Services.AddScoped<IUseCase<Guid, Category?>, GetCategoryByIdUseCase>();

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