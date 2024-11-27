namespace Phuong.eShop.CatalogService.Application.Interfaces;

public interface ICatalogDbContext
{
    DbSet<CatalogBrand> CatalogBrands { get; set; }
    
    DbSet<CatalogType> CatalogTypes { get; set; }
    
    DbSet<CatalogItem> CatalogItems { get; set; }
    DbSet<Cart> Carts { get; set; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}