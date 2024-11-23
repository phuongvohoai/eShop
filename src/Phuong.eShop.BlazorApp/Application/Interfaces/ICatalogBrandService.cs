namespace Phuong.eShop.BlazorApp.Application.Interfaces;

public interface ICatalogBrandService
{
    Task<List<CatalogBrandDto>> ListAllAsync();
    Task<CatalogBrandDto> CreateAsync(CatalogBrandDto catalogBrand);
    Task UpdateAsync(CatalogBrandDto catalogBrand);
    Task DeleteAsync(int id);
}