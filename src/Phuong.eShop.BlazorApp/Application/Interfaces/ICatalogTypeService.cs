namespace Phuong.eShop.BlazorApp.Application.Interfaces;

public interface ICatalogTypeService
{
    Task<List<CatalogTypeDto>> ListAllAsync();
    Task<CatalogTypeDto> CreateAsync(CatalogTypeDto catalogBrand);
    Task UpdateAsync(CatalogTypeDto catalogBrand);
    Task DeleteAsync(int id);
}