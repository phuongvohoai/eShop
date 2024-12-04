namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

public static class CatalogBrandErrors
{
    public static ApiError NotFound(long? id) => new("CatalogBrand.NotFound", $"Catalog brand {id} not found.");
    public static ApiError NotFound(List<long> ids) => new("CatalogBrand.NotFound", $"Catalog brand(s) not found for ID(s): {string.Join(", ", ids)}.");

}
