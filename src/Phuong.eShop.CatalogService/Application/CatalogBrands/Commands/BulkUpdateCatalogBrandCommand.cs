using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;

public record BulkUpdateCatalogBrandCommand(List<CatalogBrandDto> CatalogBrandDtos) : IRequest<ApiResponse<List<CatalogBrandDto>>>;

public class BulkUpdateCatalogBrandCommandHandler(ICatalogDbContext context, IUserService userService) : IRequestHandler<BulkUpdateCatalogBrandCommand, ApiResponse<List<CatalogBrandDto>>>
{
    public async Task<ApiResponse<List<CatalogBrandDto>>> Handle(BulkUpdateCatalogBrandCommand command, CancellationToken cancellationToken)
    {
        var selectId = command.CatalogBrandDtos.Select(d => d.Id).ToList();
        var catalogBrands = await context.CatalogBrands.Where(item => selectId.Contains(item.Id)).ToListAsync(cancellationToken);
        if (catalogBrands.Count == 0)
        {
            return CatalogBrandErrors.NotFound(selectId);
        }
        foreach (var catalogBrand in catalogBrands)
        {
            var updating = command.CatalogBrandDtos.SingleOrDefault(item => item.Id == catalogBrand.Id);
            if (updating == null)
            {
                continue;
            }
            catalogBrand.Name = updating.Name;
            catalogBrand.ModifiedBy = userService.Name;
            catalogBrand.ModifiedAt = DateTime.UtcNow;
        }
        await context.SaveChangesAsync(cancellationToken);
        return catalogBrands.Adapt<List<CatalogBrandDto>>();
    }
}





