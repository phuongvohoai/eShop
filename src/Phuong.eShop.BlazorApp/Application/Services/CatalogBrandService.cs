namespace Phuong.eShop.BlazorApp.Application.Services;

public class CatalogBrandService(IHttpClientFactory clientFactory) : CatalogApiServiceBase(clientFactory), ICatalogBrandService
{
    public async Task<List<CatalogBrandDto>> GetAllAsync()
    {
        return await GetAllAsync<CatalogBrandDto>("api/catalog/brands");
    }
    public async Task<CatalogBrandDto?> CreateAsync(CatalogBrandDto catalogBrand)
    {
        return await PostAsync("api/catalog/brands", catalogBrand);
    }

    public async Task UpdateAsync(CatalogBrandDto catalogBrand)
    {
        await PutAsync($"api/catalog/brands/{catalogBrand.Id}", catalogBrand);
    }

    public async Task DeleteAsync(long id)
    {
        await HttpClient.DeleteAsync($"api/catalog/brands/{id}");
    }
}