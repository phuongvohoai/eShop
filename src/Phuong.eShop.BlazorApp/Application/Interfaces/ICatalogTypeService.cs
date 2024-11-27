namespace Phuong.eShop.BlazorApp.Application.Interfaces;

public interface ICatalogTypeService
{
    Task<List<CatalogTypeDto>> GetAllAsync();
    Task<CatalogTypeDto?> CreateAsync(CatalogTypeDto catalogType);
    Task<CatalogTypeDto?> UpdateAsync(CatalogTypeDto catalogType);
    Task DeleteAsync(long id);
}