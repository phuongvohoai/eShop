using Phuong.eShop.CatalogService.Application.CatalogTypes.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Commands;

public record BulkUpdateCatalogTypeCommand(List<CatalogTypeDto> CatalogTypeDtos) : IRequest<ApiResponse<List<CatalogTypeDto>>>;

public class BulkUpdateCatalogTypeCommandHandler(ICatalogDbContext context, IUserService userService) : IRequestHandler<BulkUpdateCatalogTypeCommand, ApiResponse<List<CatalogTypeDto>>>
{
    public async Task<ApiResponse<List<CatalogTypeDto>>> Handle(BulkUpdateCatalogTypeCommand command, CancellationToken cancellationToken)
    {
        var selectId = command.CatalogTypeDtos.Select(d => d.Id).ToList();
        var catalogTypes = await context.CatalogTypes.Where(item => selectId.Contains(item.Id)).ToListAsync(cancellationToken);
        if (catalogTypes.Count == 0)
        {
            return CatalogTypeErrors.NotFound(selectId);
        }
        foreach (var catalogType in catalogTypes)
        {
            var updating = command.CatalogTypeDtos.SingleOrDefault(item => item.Id == catalogType.Id);
            if (updating == null)
            {
                continue;
            }
            catalogType.Name = updating.Name;
            catalogType.ModifiedAt = DateTime.UtcNow;
            catalogType.ModifiedBy = userService.Name;
        }
        await context.SaveChangesAsync(cancellationToken);
        return catalogTypes.Adapt<List<CatalogTypeDto>>();
    }
}





