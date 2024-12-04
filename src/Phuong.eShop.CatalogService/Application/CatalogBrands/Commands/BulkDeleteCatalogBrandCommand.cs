using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;

public record BulkDeleteCatalogBrandCommand(List<long> Ids) : IRequest<ApiResponse<bool>>;

public class BulkDeteCatalogBrandCommandHandler(ICatalogDbContext context) : IRequestHandler<BulkDeleteCatalogBrandCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(BulkDeleteCatalogBrandCommand command, CancellationToken cancellationToken)
    {
        var catalogBrands = await context.CatalogBrands.Where(item => command.Ids.Contains(item.Id)).ToListAsync(cancellationToken);
        if (catalogBrands.Count == 0)
        {
            return CatalogBrandErrors.NotFound(command.Ids);
        }
        context.CatalogBrands.RemoveRange(catalogBrands);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

