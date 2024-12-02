namespace Phuong.eShop.BlazorApp.Application.Models;

public class CatalogProductDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? PictureUri { get; set; }
    public long CatalogTypeId { get; set; }
    public long CatalogBrandId { get; set; }
    public string? CatalogType { get; set; }
    public string? CatalogBrand { get; set; }
    public int AvailableStock { get; set; }

    private CatalogBrandDto? _catalogBrandNavigation;

    public CatalogBrandDto? CatalogBrandNavigation
    {
        get => _catalogBrandNavigation;
        set
        {
            _catalogBrandNavigation = value;
            CatalogBrand = value?.Name;
            CatalogBrandId = value?.Id ?? 0;
        }
    }
    
    private CatalogTypeDto? _catalogTypeNavigation;
    public CatalogTypeDto? CatalogTypeNavigation
    {
        get => _catalogTypeNavigation;
        set
        {
            _catalogTypeNavigation = value;
            CatalogType = value?.Name;
            CatalogTypeId = value?.Id ?? 0;
        }
    }
}