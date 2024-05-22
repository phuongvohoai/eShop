using Phuong.eShop.CatalogService.Application.Common;

namespace Phuong.eShop.CatalogService.Application.CatalogItems.Queries;

public record GetCatalogItemWithPaginationQuery : IRequest<PaginatedList<CatalogItemDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCatalogItemWithPaginationQueryHandler(ICatalogDbContext context) : IRequestHandler<GetCatalogItemWithPaginationQuery, PaginatedList<CatalogItemDto>>
{
    public async Task<PaginatedList<CatalogItemDto>> Handle(GetCatalogItemWithPaginationQuery request, CancellationToken cancellationToken)
    {
        TypeAdapterConfig<CatalogItem, CatalogItemDto>
            .NewConfig()
            .Map(dest => dest.CatalogBrand, src => src.CatalogBrand.Name)
            .Map(dest => dest.CatalogType, src => src.CatalogType.Name);
        
        return await context.CatalogItems
            .Include(s => s.CatalogBrand)
            .Include(s => s.CatalogType)
            .ProjectToType<CatalogItemDto>()
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}