using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;
namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Queries;

public record GetCatalogBrandsQuery : IRequest<List<CatalogBranchDto>>;

public class GetCatalogBrandsQueryHandler(ICatalogDbContext context) : IRequestHandler<GetCatalogBrandsQuery, List<CatalogBranchDto>>
{
    public async Task<List<CatalogBranchDto>> Handle(GetCatalogBrandsQuery request, CancellationToken cancellationToken)
    {
        return await context.CatalogBrands
            .AsNoTracking()
            .ProjectToType<CatalogBranchDto>()
            .ToListAsync(cancellationToken);
    }
}