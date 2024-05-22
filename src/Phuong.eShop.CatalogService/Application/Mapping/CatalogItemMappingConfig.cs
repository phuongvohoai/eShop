using Phuong.eShop.CatalogService.Application.CatalogItems.Queries;

namespace Phuong.eShop.CatalogService.Application.Mapping;

public class CatalogItemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CatalogItem, CatalogItemDto>()
            .Map(dest => dest.CatalogBrand, src => src.CatalogBrand.Name)
            .Map(dest => dest.CatalogType, src => src.CatalogType.Name);
    }
}