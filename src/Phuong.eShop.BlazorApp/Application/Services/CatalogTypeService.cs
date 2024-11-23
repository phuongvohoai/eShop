using Phuong.eShop.BlazorApp.Application.Interfaces;

namespace Phuong.eShop.BlazorApp.Application.Services;

public class CatalogTypeService : ICatalogTypeService
{
    public Task<List<CatalogTypeDto>> ListAllAsync()
    {
        return Task.FromResult<List<CatalogTypeDto>>(new List<CatalogTypeDto>()
        {
            new CatalogTypeDto()
            {
                Id = 1,
                Name = "Type 1"
            },
            new CatalogTypeDto()
            {
                Id = 2,
                Name = "Type 2"
            },
            new CatalogTypeDto()
            {
                Id = 3,
                Name = "Type 3"
            },
        });
    }
    
    public Task<CatalogTypeDto> CreateAsync(CatalogTypeDto catalogBrand)
    {
        return null;
    }

    public Task UpdateAsync(CatalogTypeDto catalogBrand)
    {
        return null;
    }

    public Task DeleteAsync(int id)
    {
        return null;
    }
}