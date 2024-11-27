using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;
namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Queries;
public record GetCatalogBrandsQuery : IRequest<ApiResponse<List<CatalogBrandDto>>>;
public class GetCatalogBrandsQueryHandler(ICatalogDbContext context) : IRequestHandler<GetCatalogBrandsQuery, ApiResponse<List<CatalogBrandDto>>>
{
    public async Task<ApiResponse<List<CatalogBrandDto>>> Handle(GetCatalogBrandsQuery request, CancellationToken cancellationToken)
    {
        var catalogBrand = await context.CatalogBrands.AsNoTracking().ToListAsync(cancellationToken);
        return catalogBrand.Adapt<List<CatalogBrandDto>>();
    }
}