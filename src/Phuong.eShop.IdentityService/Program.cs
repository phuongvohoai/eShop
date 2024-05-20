using Microsoft.AspNetCore.Identity;
using Phuong.eShop.Identity.Application.Entities;
using Phuong.eShop.Identity.Infrastructure.Persistence;
using Phuong.eShop.ServiceDefaults.Extensions;
using Phuong.eShop.ServiceDefaults.Migration;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiServices();

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.AddNpgsqlDbContext<AppDbContext>("identityDb");
builder.Services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<AppDbContext>().AddApiEndpoints();

builder.Services.AddMigration<AppDbContext, AppDbContextSeed>();

var app = builder.Build();

app.ConfigureApiServiceMiddleware();

app.MapIdentityApi<ApplicationUser>();

await app.RunAsync();