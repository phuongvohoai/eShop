using Phuong.eShop.CatalogService.Application.CatalogTypes.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Queries;

public record GetCatalogTypesQuery : IRequest<ApiResponse<List<CatalogTypeDto>>>;

public class GetCatalogTypesQueryHandler(ICatalogDbContext context) : IRequestHandler<GetCatalogTypesQuery, ApiResponse<List<CatalogTypeDto>>>
{
    public async Task<ApiResponse<List<CatalogTypeDto>>> Handle(GetCatalogTypesQuery request, CancellationToken cancellationToken)
    {
        var value = await context.CatalogTypes
             .AsNoTracking()
             .ToListAsync(cancellationToken);
        return value.Adapt<List<CatalogTypeDto>>();
    }
}