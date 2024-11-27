using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Queries
{
    public record  GetCatalogBrandQueryById (long id) : IRequest<ApiResponse<CatalogBrandDto>>;

    public class GetCatalogBrandQueryByIdHandler (ICatalogDbContext context) : IRequestHandler<GetCatalogBrandQueryById, ApiResponse<CatalogBrandDto>>
    {
        public async Task<ApiResponse<CatalogBrandDto>> Handle(GetCatalogBrandQueryById request, CancellationToken cancellationToken)
        {
            var value = await context.CatalogBrands
                .AsNoTracking ()
                .FirstOrDefaultAsync(e => e.Id == request.id);
            if (value == null) { return CatalogBrandErrors.NotFound(request.id); }
            return value.Adapt<CatalogBrandDto> ();
        }
    }

}
