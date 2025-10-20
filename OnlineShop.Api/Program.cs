using FluentValidation;
using FluentValidation.AspNetCore;
using OnlineShop.Core;
using OnlineShop.Core.Validators;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();

var jsonFilePath = Path.Combine(builder.Environment.ContentRootPath, "categories.json");
builder.Services
    .AddDataAccess(jsonFilePath)
    .AddApplicationServices()
    .AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("OnlineShop.Data")
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();