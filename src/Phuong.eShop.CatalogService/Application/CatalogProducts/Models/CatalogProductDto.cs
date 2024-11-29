namespace Phuong.eShop.CatalogService.Application.CatalogProducts.Models;

public class CatalogProductDto
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public string? PictureUri { get; set; }
    public long CatalogTypeId { get; set; }
    public long CatalogBrandId { get; set; }
    public string? CatalogType { get; set; }
    public string? CatalogBrand { get; set; }
    public int AvailableStock { get; set; }
}
