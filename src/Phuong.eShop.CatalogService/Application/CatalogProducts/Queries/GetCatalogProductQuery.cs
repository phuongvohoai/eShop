using Phuong.eShop.CatalogService.Application.CatalogProducts.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogProducts.Queries
{
    public record GetCatalogProductQuery : IRequest<ApiResponse<List<CatalogProductDto>>>;
    public class GetCatalogProductQueryHandler(ICatalogDbContext context) : IRequestHandler<GetCatalogProductQuery, ApiResponse<List<CatalogProductDto>>>
    {
        public async Task<ApiResponse<List<CatalogProductDto>>> Handle(GetCatalogProductQuery request, CancellationToken cancellationToken)
        {
            var catalogProduct = await context.CatalogItems.AsNoTracking().ToListAsync();
            return catalogProduct.Adapt<List<CatalogProductDto>>();
        }
    }
}

