using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eShop.Infrastructure.Identity;

public class EShopIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public EShopIdentityDbContext()
    {
        
    }

    public EShopIdentityDbContext(DbContextOptions<EShopIdentityDbContext> options) : base(options)
    {
        
    }
}
