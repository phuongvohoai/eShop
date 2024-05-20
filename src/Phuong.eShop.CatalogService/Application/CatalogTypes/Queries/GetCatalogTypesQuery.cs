namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Queries;

public record GetCatalogTypesQuery : IRequest<List<CatalogTypeDto>>;

public class GetCatalogTypesQueryHandler(ICatalogDbContext context) : IRequestHandler<GetCatalogTypesQuery, List<CatalogTypeDto>>
{
    public async Task<List<CatalogTypeDto>> Handle(GetCatalogTypesQuery request, CancellationToken cancellationToken)
    {
        return await context.CatalogTypes
            .AsNoTracking()
            .ProjectToType<CatalogTypeDto>()
            .ToListAsync(cancellationToken);
    }
}