using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Services;
using OnlineShop.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jsonFilePath = Path.Combine(builder.Environment.ContentRootPath, "categories.json");

builder.Services.AddSingleton<ICategoryRepository>(
    new JsonCategoryRepository(jsonFilePath)
);

builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();