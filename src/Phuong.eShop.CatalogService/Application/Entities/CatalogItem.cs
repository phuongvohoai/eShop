namespace Phuong.eShop.CatalogService.Application.Entities;

public class CatalogItem : Auditable
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string PictureUri { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public long CatalogTypeId { get; set; }
    public long CatalogBrandId { get; set; }
    public CatalogType CatalogType { get; set; } = null!;
    public CatalogBrand CatalogBrand { get; set; } = null!;
    public int AvailableStock { get; set; }
}


public class CatalogCsvItem
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public  string? PictureUri { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public long CatalogTypeId { get; set; }
    public long CatalogBrandId { get; set; }
    public int AvailableStock { get; set; }
}