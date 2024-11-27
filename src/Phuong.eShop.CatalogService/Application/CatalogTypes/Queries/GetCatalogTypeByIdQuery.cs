using Phuong.eShop.CatalogService.Application.CatalogTypes.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Queries
{
    public record GetCatalogTypeByIdQuery(long Id) : IRequest<ApiResponse<CatalogTypeDto>>;
    public class GetCatalogTypeByIdQueryHandler(ICatalogDbContext context) : IRequestHandler<GetCatalogTypeByIdQuery, ApiResponse<CatalogTypeDto>>
    {
        public async Task<ApiResponse<CatalogTypeDto>> Handle(GetCatalogTypeByIdQuery request, CancellationToken cancellation)
        {
            var catalogType = await context.CatalogTypes.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id, cancellation);
            if (catalogType == null)
            {
                return CatalogTypeErrors.NotFound(request.Id);
            }
            return catalogType.Adapt<CatalogTypeDto>();
        }
    }
}

