namespace Phuong.eShop.CatalogService.Application.Entities;

public class CatalogType : Auditable
{
    public long Id { get; set; }
    public required string Name { get; set; }
}