namespace Phuong.eShop.CatalogService.Application.Entities
{
    public class ProductFile
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public required byte[] Content { get; set; }
    }
}
