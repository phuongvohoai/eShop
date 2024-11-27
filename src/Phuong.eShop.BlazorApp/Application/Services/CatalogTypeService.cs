namespace Phuong.eShop.BlazorApp.Application.Services;

public class CatalogTypeService(IHttpClientFactory clientFactory) : CatalogApiServiceBase(clientFactory), ICatalogTypeService
{
    public async Task<List<CatalogTypeDto>> GetAllAsync()
    {
        return await GetAllAsync<CatalogTypeDto>("api/catalog/types");
    }

    public async Task<CatalogTypeDto?> CreateAsync(CatalogTypeDto catalogType)
    {
        return await PostAsync<CatalogTypeDto>("api/catalog/types", catalogType);
    }

    public async Task<CatalogTypeDto?> UpdateAsync(CatalogTypeDto catalogType)
    {
        return await PutAsync<CatalogTypeDto>($"api/catalog/types/{catalogType.Id}", catalogType);
    }

    public async Task DeleteAsync(long id)
    {
        await HttpClient.DeleteAsync($"api/catalog/types/{id}");
    }
}