namespace Phuong.eShop.CatalogService.Application.CatalogItems.Queries;

public record GetCatalogItemByIdQuery(int Id) : IRequest<CatalogItemDto?>;

public class GetCatalogItemByIdQueryHandler(ICatalogDbContext context) : IRequestHandler<GetCatalogItemByIdQuery, CatalogItemDto?>
{
    public async Task<CatalogItemDto?> Handle(GetCatalogItemByIdQuery request, CancellationToken cancellationToken)
    {
        TypeAdapterConfig<CatalogItem, CatalogItemDto>
            .NewConfig()
            .Map(dest => dest.CatalogBrand, src => src.CatalogBrand.Name)
            .Map(dest => dest.CatalogType, src => src.CatalogType.Name);

        return await context.CatalogItems
            .Include(s => s.CatalogBrand)
            .Include(s => s.CatalogType)
            .Where(e => e.Id == request.Id)
            .ProjectToType<CatalogItemDto>()
            .FirstOrDefaultAsync(cancellationToken);
    }
}