namespace Phuong.eShop.CatalogService.Application.CatalogProducts.Models
{
    public class CatalogProductErrors
    {
        public static ApiError NotFound(long? id) => new("CatalogProduct.NotFound", $"Catalog product {id} not found.");

    }
}
