namespace Phuong.eShop.CatalogService.Application.Entities;

public class CatalogBrand : Auditable
{
    public long Id { get; set; }
    public required string Name { get; set; }
}