namespace Phuong.eShop.BlazorApp.Application.Services;

public class CatalogService(IHttpClientFactory clientFactory) : CatalogApiServiceBase(clientFactory), ICatalogService
{
    public async Task<List<CatalogItemDto>> GetAllAsync()
    {
        return await GetAllAsync<CatalogItemDto>("api/catalog/items");
    }

    public async Task<CatalogItemDto?> CreateAsync(CatalogItemDto catalogBrand)
    {
        return await PostAsync<CatalogItemDto?>("api/catalog/items", catalogBrand);
    }

    public async Task<CatalogItemDto?> UpdateAsync(CatalogItemDto catalogBrand)
    {
        return await PutAsync<CatalogItemDto?>($"api/catalog/items/{catalogBrand.Id}", catalogBrand);
    }

    public async Task DeleteAsync(long id)
    {
        await HttpClient.DeleteAsync($"api/catalog/items/{id}");
    }
}