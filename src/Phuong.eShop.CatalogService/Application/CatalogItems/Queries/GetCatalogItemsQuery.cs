namespace Phuong.eShop.CatalogService.Application.CatalogItems.Queries;

public record GetCatalogItemsQuery : IRequest<List<CatalogItemDto>>;

public class GetCatalogItemsQueryHandler(ICatalogDbContext context) : IRequestHandler<GetCatalogItemsQuery, List<CatalogItemDto>>
{
    public async Task<List<CatalogItemDto>> Handle(GetCatalogItemsQuery request, CancellationToken cancellationToken)
    {
        return await context.CatalogItems
            .Include(s => s.CatalogBrand)
            .Include(s => s.CatalogType)
            .AsNoTracking()
            .ProjectToType<CatalogItemDto>()
            .ToListAsync(cancellationToken);
    }
}