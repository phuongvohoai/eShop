using Phuong.eShop.BlazorApp.Application.Interfaces;

namespace Phuong.eShop.BlazorApp.Application.Services;

public class CatalogTypeService(IHttpClientFactory clientFactory) : CatalogApiServiceBase(clientFactory), ICatalogTypeService
{
    public async Task<List<CatalogTypeDto>> GetAllAsync()
    {
        return await GetAllAsync<CatalogTypeDto>("api/catalog/types");
    }
    
    public async Task<CatalogTypeDto?> CreateAsync(CatalogTypeDto catalogType)
    {
        return await PostAsync("api/catalog/types", catalogType);
    }

    public async Task UpdateAsync(CatalogTypeDto catalogType)
    {
        await PutAsync($"api/catalog/types/{catalogType.Id}", catalogType);
    }

    public async Task DeleteAsync(int id)
    {
        await HttpClient.DeleteAsync($"api/catalog/types/{id}");
    }
}