using System.Reflection;
using Phuong.eShop.CatalogService.Infrastructure.Persistence;
using Phuong.eShop.ServiceDefaults.Extensions;
using Phuong.eShop.ServiceDefaults.Migration;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiServicesWithMediatR(Assembly.GetExecutingAssembly());

builder.AddNpgsqlDbContext<CatalogDbContext>("catalogDb");
builder.Services.AddMigration<CatalogDbContext, CatalogSeed>();
builder.Services.AddScoped<ICatalogDbContext>(sp => sp.GetRequiredService<CatalogDbContext>());
builder.Services.AddMappings(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.ConfigureApiServiceMiddleware();

await app.RunAsync();