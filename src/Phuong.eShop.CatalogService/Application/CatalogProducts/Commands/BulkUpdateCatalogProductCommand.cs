using Phuong.eShop.CatalogService.Application.CatalogProducts.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogProducts.Commands;

public record BulkUpdateCatalogProductCommand(List<CatalogProductDto> CatalogProductDtos) : IRequest<ApiResponse<List<CatalogProductDto>>>;

public class BulkUpdateCatalogProductCommandHandler(ICatalogDbContext context, IUserService userService) : IRequestHandler<BulkUpdateCatalogProductCommand, ApiResponse<List<CatalogProductDto>>>
{
    public async Task<ApiResponse<List<CatalogProductDto>>> Handle(BulkUpdateCatalogProductCommand command, CancellationToken cancellationToken)
    {
        var selectId = command.CatalogProductDtos.Select(d => d.Id).ToList();
        var catalogProducts = await context.CatalogItems.Where(item => selectId.Contains(item.Id)).ToListAsync(cancellationToken);
        if (catalogProducts.Count == 0)
        {
            return CatalogProductErrors.NotFound(selectId);
        }
        foreach (var catalogProduct in catalogProducts)
        {
            var updating = command.CatalogProductDtos.SingleOrDefault(item => item.Id == catalogProduct.Id);
            if (updating == null)
            {
                continue;
            }
            catalogProduct.Name = updating.Name;
            catalogProduct.Description = updating.Description;
            catalogProduct.Price = updating.Price;
            catalogProduct.AvailableStock = updating.AvailableStock;
            catalogProduct.CatalogBrandId = updating.CatalogBrandId;
            catalogProduct.CatalogTypeId = updating.CatalogTypeId;
            catalogProduct.ModifiedAt = DateTime.UtcNow;
            catalogProduct.ModifiedBy = userService.Name;
        }
        await context.SaveChangesAsync(cancellationToken);
        return catalogProducts.Adapt<List<CatalogProductDto>>();
    }
}

