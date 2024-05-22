using Phuong.eShop.CatalogService.Application.Common;

namespace Phuong.eShop.CatalogService.Application.CatalogItems.Queries;

public record GetCatalogItemWithPaginationQuery : IRequest<PaginatedList<CatalogItemDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public int Type { get; set; }
    public int Brand { get; set; }
    public string SearchString { get; set; } = "";
}

public class GetCatalogItemWithPaginationQueryHandler(ICatalogDbContext context)
    : IRequestHandler<GetCatalogItemWithPaginationQuery, PaginatedList<CatalogItemDto>>
{
    public async Task<PaginatedList<CatalogItemDto>> Handle(GetCatalogItemWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var queryable = context.CatalogItems
            .Include(s => s.CatalogBrand)
            .Include(s => s.CatalogType)
            .AsQueryable();

        if (request.Type > 0)
        {
            queryable = queryable.Where(x => x.CatalogTypeId == request.Type);
        }

        if (request.Brand > 0)
        {
            queryable = queryable.Where(x => x.CatalogBrandId == request.Brand);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchString))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(request.SearchString.ToLower()));
        }

        return await queryable
            .ProjectToType<CatalogItemDto>()
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}