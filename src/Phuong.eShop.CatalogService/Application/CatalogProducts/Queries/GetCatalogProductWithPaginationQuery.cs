using Phuong.eShop.CatalogService.Application.CatalogProducts.Models;
using Phuong.eShop.CatalogService.Application.Common;

namespace Phuong.eShop.CatalogService.Application.CatalogProducts.Queries;

public record GetCatalogProductWithPaginationQuery : IRequest<ApiResponse<PaginatedList<CatalogProductDto>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int Type { get; set; }
    public int Brand { get; set; }
    public string SearchString { get; set; } = "";
}

public class GetCatalogProductWithPaginationQueryHandler(ICatalogDbContext context)
    : IRequestHandler<GetCatalogProductWithPaginationQuery, ApiResponse<PaginatedList<CatalogProductDto>>>
{
    public async Task<ApiResponse<PaginatedList<CatalogProductDto>>> Handle(GetCatalogProductWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var catalogProducts = context.CatalogItems
            .Include(x => request.Type)
            .Include(x => request.Brand)
            .AsQueryable();

        if (request.Type > 0)
        {
            catalogProducts = catalogProducts.Where(x => x.CatalogTypeId == request.Type);
        }

        if (request.Brand > 0)
        {
            catalogProducts = catalogProducts.Where(x => x.CatalogBrandId == request.Brand);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchString))
        {
            catalogProducts = catalogProducts.Where(x => x.Name.ToLower().Contains(request.SearchString.ToLower()));
        }

        var totalItemCount = await catalogProducts.CountAsync(cancellationToken);
        var pagedData = await catalogProducts
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ProjectToType<CatalogProductDto>()
            .ToListAsync(cancellationToken);
        var paginatedResult = new PaginatedList<CatalogProductDto>(pagedData, totalItemCount, request.PageNumber, request.PageSize);
        return paginatedResult.Adapt<PaginatedList<CatalogProductDto>>();
    }
}