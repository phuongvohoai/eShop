namespace Phuong.eShop.BlazorApp.Application.Models;

public record CatalogItemDto(
    long Id,
    string Name,
    string Description,
    decimal Price,
    string PictureUri,
    long CatalogTypeId,
    long CatalogBrandId,
    string CatalogType,
    string CatalogBrand,
    int AvailableStock);