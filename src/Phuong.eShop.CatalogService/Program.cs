using System.Reflection;
using Phuong.eShop.CatalogService.Application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Phuong.eShop.CatalogService.Infrastructure.Persistence;
using Phuong.eShop.ServiceDefaults.Extensions;
using Phuong.eShop.ServiceDefaults.Migration;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiServicesWithMediatR(Assembly.GetExecutingAssembly());

builder.AddNpgsqlDbContext<CatalogDbContext>("catalogDb");
builder.Services.AddMigration<CatalogDbContext, CatalogSeed>();
builder.Services.AddScoped<ICatalogDbContext>(sp => sp.GetRequiredService<CatalogDbContext>());
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddMappings(Assembly.GetExecutingAssembly());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                o.MetadataAddress = "http://localhost:8080/realms/eshop/.well-known/openid-configuration";
                o.Authority = "http://localhost:8080/realms/eshop";
                o.Audience = "account";
                o.RequireHttpsMetadata = false;
            });
builder.Services.AddCors();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureApiServiceMiddleware();

app.UseCors(opt => opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

await app.RunAsync();