namespace Phuong.eShop.BlazorApp.Application.Interfaces;

public interface ICatalogBrandService
{
    Task<List<CatalogBrandDto>> GetAllAsync();
    Task<CatalogBrandDto?> CreateAsync(CatalogBrandDto catalogBrand);
    Task UpdateAsync(CatalogBrandDto catalogBrand);
    Task DeleteAsync(long id);
}