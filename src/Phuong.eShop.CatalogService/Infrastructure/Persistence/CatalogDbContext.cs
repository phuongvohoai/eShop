using System.Reflection;

namespace Phuong.eShop.CatalogService.Infrastructure.Persistence;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options), ICatalogDbContext
{
    public DbSet<CatalogBrand> CatalogBrands { get; set; }
    public DbSet<CatalogType> CatalogTypes { get; set; }
    public DbSet<CatalogItem> CatalogItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<ProductFile> ProductFiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}