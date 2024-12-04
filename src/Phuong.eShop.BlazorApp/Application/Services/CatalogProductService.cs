namespace Phuong.eShop.BlazorApp.Application.Services;

public class CatalogProductService(IHttpClientFactory clientFactory) : CatalogApiServiceBase(clientFactory), ICatalogProductService
{
    public async Task<List<CatalogProductDto>> GetAllAsync()
    {
        return await GetAllAsync<CatalogProductDto>("api/catalog/products");
    }

    public async Task<CatalogProductDto?> CreateAsync(CatalogProductDto catalogBrand)
    {
        return await PostAsync<CatalogProductDto?>("api/catalog/products", catalogBrand);
    }

    public async Task<CatalogProductDto?> UpdateAsync(CatalogProductDto catalogBrand)
    {
        return await PutAsync<CatalogProductDto?>($"api/catalog/products/{catalogBrand.Id}", catalogBrand);
    }

    public async Task DeleteAsync(long id)
    {
        await HttpClient.DeleteAsync($"api/catalog/products/{id}");
    }
}