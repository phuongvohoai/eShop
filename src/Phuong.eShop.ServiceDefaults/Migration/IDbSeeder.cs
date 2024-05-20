using Microsoft.EntityFrameworkCore;

namespace Phuong.eShop.ServiceDefaults.Migration;

public interface IDbSeeder<in TDbContext> where TDbContext : DbContext
{
    Task SeedAsync(TDbContext dbContext, CancellationToken cancellationToken = default);
}
