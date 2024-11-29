using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Queries
{
    public record GetCatalogBrandByIdQuery(long Id) : IRequest<ApiResponse<CatalogBrandDto>>;\

    public class GetCatalogBrandByIdHandlerQuery(ICatalogDbContext context) : IRequestHandler<GetCatalogBrandByIdQuery, ApiResponse<CatalogBrandDto>>
    {
        public async Task<ApiResponse<CatalogBrandDto>> Handle(GetCatalogBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var catalogBrand = await context.CatalogBrands.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (catalogBrand == null)
            {
                return CatalogBrandErrors.NotFound(request.Id);
            }
            return catalogBrand.Adapt<CatalogBrandDto>();
        }
    }
}
