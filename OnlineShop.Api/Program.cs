using OnlineShop.Core;
using OnlineShop.Core.Options;
using OnlineShop.Core.Services;
using OnlineShop.Extensions;
using OnlineShop.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationServices();

builder.Services.AddDataAccess(builder.Configuration)
    .AddApplicationServices()
    .AddControllers();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSingleton<TokenService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();