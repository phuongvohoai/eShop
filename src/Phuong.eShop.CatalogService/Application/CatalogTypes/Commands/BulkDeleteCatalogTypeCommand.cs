using Phuong.eShop.CatalogService.Application.CatalogTypes.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Commands;

public record BulkDeleteCatalogTypeCommand(List<long> Ids) : IRequest<ApiResponse<bool>>;

public class BulkDeleteCatalogTypeCommandHandler(ICatalogDbContext context) : IRequestHandler<BulkDeleteCatalogTypeCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(BulkDeleteCatalogTypeCommand command, CancellationToken cancellationToken)
    {
        var catalogTypes = await context.CatalogTypes.Where(item => command.Ids.Contains(item.Id)).ToListAsync(cancellationToken);
        if (catalogTypes.Count == 0)
        {
            return CatalogTypeErrors.NotFound(command.Ids);
        }
        context.CatalogTypes.RemoveRange(catalogTypes);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}

