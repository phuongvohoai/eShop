using Phuong.eShop.CatalogService.Application.CatalogProducts.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogProducts.Commands;

public record BulkDeleteCatalogProductCommand(List<long> Ids) : IRequest<ApiResponse<bool>>;

public class BulkDeleteCatalogProductCommandHandler(ICatalogDbContext context) : IRequestHandler<BulkDeleteCatalogProductCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(BulkDeleteCatalogProductCommand command, CancellationToken cancellationToken)
    {
        var catalogProducts = await context.CatalogItems.Where(item => command.Ids.Contains(item.Id)).ToListAsync(cancellationToken);
        if (catalogProducts.Count == 0)
        {
            return CatalogProductErrors.NotFound(command.Ids);
        }
        context.CatalogItems.RemoveRange(catalogProducts);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}


