namespace Phuong.eShop.CatalogService.Application.CatalogItems.Queries;

public record GetCatalogItemByIdQuery(int Id) : IRequest<CatalogItemDto?>;

public class GetCatalogItemByIdQueryHandler(ICatalogDbContext context) : IRequestHandler<GetCatalogItemByIdQuery, CatalogItemDto?>
{
    public async Task<CatalogItemDto?> Handle(GetCatalogItemByIdQuery request, CancellationToken cancellationToken)
    {
        return await context.CatalogItems
            .Include(s => s.CatalogBrand)
            .Include(s => s.CatalogType)
            .Where(e => e.Id == request.Id)
            .ProjectToType<CatalogItemDto>()
            .FirstOrDefaultAsync(cancellationToken);
    }
}