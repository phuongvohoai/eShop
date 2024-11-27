namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

public static class CatalogBrandErrors
{
    public static ApiError NotFound(long? id) => new("CatalogBrand.NotFound", $"Catalog brand {id} not found.");
}
