using FluentValidation;
using FluentValidation.AspNetCore;
using OnlineShop.Core;
using OnlineShop.Core.Validators;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var dbHost = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
var dbPort = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
var dbName = Environment.GetEnvironmentVariable("POSTGRES_DB");
var dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
var dbPass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

var connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPass}";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("OnlineShop.Data")));

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();