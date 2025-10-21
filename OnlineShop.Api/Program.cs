using FluentValidation;
using FluentValidation.AspNetCore;
using OnlineShop.Core;
using OnlineShop.Core.Validators;
using OnlineShop.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();

var jsonFilePath = Path.Combine(builder.Environment.ContentRootPath, "categories.json");

builder.Services
    .AddDataAccess(builder.Configuration, jsonFilePath)
    .AddApplicationServices()
    .AddControllers();

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