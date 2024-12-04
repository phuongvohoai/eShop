namespace Phuong.eShop.CatalogService.Application.CatalogProducts.Models
{
    public class CatalogProductErrors
    {
        public static ApiError NotFound(long? id) => new("CatalogProduct.NotFound", $"Catalog product {id} not found.");
        public static ApiError NotFound(List<long> ids) => new("CatalogProduct.NotFound", $"Catalog product(s) not found for ID(s): {string.Join(", ", ids)}.");
    }
}
