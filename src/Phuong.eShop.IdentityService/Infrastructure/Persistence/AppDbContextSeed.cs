using Microsoft.AspNetCore.Identity;
using Phuong.eShop.Identity.Application.Entities;
using Phuong.eShop.ServiceDefaults.Migration;

namespace Phuong.eShop.Identity.Infrastructure.Persistence;

public sealed class AppDbContextSeed(UserManager<ApplicationUser> userManager, ILogger<AppDbContextSeed> logger) : IDbSeeder<AppDbContext>
{
    public async Task SeedAsync(AppDbContext dbContext, CancellationToken cancellationToken = default)
    {
        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@email.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "Password1!");

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug("Admin user created");
            }
        }
    }
}