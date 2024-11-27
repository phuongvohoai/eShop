using Microsoft.AspNetCore.Identity;
using Phuong.eShop.Identity.Application.Entities;
using Phuong.eShop.Identity.Infrastructure.Persistence;
using Phuong.eShop.ServiceDefaults.Extensions;
using Phuong.eShop.ServiceDefaults.Migration;

var builder = WebApplication.CreateBuilder(args);

// Denpendency Inject (SOLID) Container
builder.AddApiServices();

builder.Services.AddAuthentication(IdentityConstants.BearerScheme)
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.AddNpgsqlDbContext<AppDbContext>("identityDb");
builder.Services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<AppDbContext>().AddApiEndpoints();

builder.Services.AddMigration<AppDbContext, AppDbContextSeed>();

builder.Services.AddCors();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureApiServiceMiddleware();

app.MapIdentityApi<ApplicationUser>();

app.UseCors(opt => opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

await app.RunAsync();