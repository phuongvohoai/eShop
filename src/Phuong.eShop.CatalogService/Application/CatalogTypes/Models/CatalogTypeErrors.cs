namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Models
{
    public static class CatalogTypeErrors
    {
        public static ApiError NotFound(long? id) => new("CatalogType.NotFound", $"Catalog type {id} not found.");
        public static ApiError NotFound(List<long> ids) => new("CatalogType.NotFound", $"Catalog type(s) not found for ID(s): {string.Join(", ", ids)}.");
    }
}