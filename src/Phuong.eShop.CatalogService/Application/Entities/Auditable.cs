namespace Phuong.eShop.CatalogService.Application.Entities
{
    public class Auditable
    {
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
