using Microsoft.AspNetCore.Http.HttpResults;
using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;
using Phuong.eShop.CatalogService.Application.Common;
namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;
public record UpdateCatalogBrandCommand(string Name) : IRequest<ApiResponse<CatalogBrandDto>>
{
    public long Id { get; set; }
}
public class UpdateCatalogBrandCommandHandler(ICatalogDbContext context, IUserService userService) : IRequestHandler<UpdateCatalogBrandCommand, ApiResponse<CatalogBrandDto>>
{
    public async Task<ApiResponse<CatalogBrandDto>> Handle(UpdateCatalogBrandCommand request, CancellationToken cancellationToken)
    {
        var catalogBrand = await context.CatalogBrands.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (catalogBrand == null)
        {
            return CatalogBrandErrors.NotFound(request.Id);
        }
        catalogBrand.Name = request.Name;
        catalogBrand.ModifiedBy = userService.Name;
        catalogBrand.ModifiedAt = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);
        return catalogBrand.Adapt<CatalogBrandDto>();
    }
}
