namespace Phuong.eShop.BlazorApp.Application.Interfaces;

public interface ICatalogProductService
{
    Task<List<CatalogProductDto>> GetAllAsync();
    Task<CatalogProductDto?> CreateAsync(CatalogProductDto catalogProduct);
    Task<CatalogProductDto?> UpdateAsync(CatalogProductDto catalogProduct);
    Task DeleteAsync(long id);
}