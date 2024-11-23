using Phuong.eShop.BlazorApp.Application.Interfaces;

namespace Phuong.eShop.BlazorApp.Application.Services;

public class CatalogBrandService : ICatalogBrandService
{
    public Task<List<CatalogBrandDto>> ListAllAsync()
    {
        return Task.FromResult(new List<CatalogBrandDto>()
        {
            new CatalogBrandDto() { Id = 1, Name = "Brand 1" },
            new CatalogBrandDto() { Id = 2, Name = "Brand 2" },
            new CatalogBrandDto() { Id = 3, Name = "Brand 3" },
            new CatalogBrandDto() { Id = 4, Name = "Brand 4" },
        });
    }
    public Task<CatalogBrandDto> CreateAsync(CatalogBrandDto catalogBrand)
    {
        return null;
    }

    public Task UpdateAsync(CatalogBrandDto catalogBrand)
    {
        return null;
    }

    public Task DeleteAsync(int id)
    {
        return null;
    }
}