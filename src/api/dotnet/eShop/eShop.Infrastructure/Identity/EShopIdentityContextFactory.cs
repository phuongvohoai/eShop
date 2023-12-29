using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eShop.Infrastructure.Identity;

public class EShopIdentityContextFactory : IDesignTimeDbContextFactory<EShopIdentityDbContext>
{
    public EShopIdentityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<EShopIdentityDbContext>();
        optionsBuilder.UseSqlServer("");

        return new EShopIdentityDbContext(optionsBuilder.Options);
    }
}
