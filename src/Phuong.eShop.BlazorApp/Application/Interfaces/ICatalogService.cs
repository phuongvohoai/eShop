namespace Phuong.eShop.BlazorApp.Application.Interfaces;

public interface ICatalogService
{
    Task<List<CatalogItemDto>> GetAllAsync();
    Task<CatalogItemDto?> CreateAsync(CatalogItemDto catalogItem);
    Task<CatalogItemDto?> UpdateAsync(CatalogItemDto catalogItem);
    Task DeleteAsync(long id);
}