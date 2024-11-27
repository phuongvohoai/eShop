using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;
namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;
public record UpdateCatalogBrandCommand(string Name) : IRequest<ApiResponse<CatalogBrandDto>>
{
    public long Id { get; set; }
}
public class UpdateCatalogBrandCommandHandler(ICatalogDbContext context) : IRequestHandler<UpdateCatalogBrandCommand, ApiResponse<CatalogBrandDto>>
{
    public async Task<ApiResponse<CatalogBrandDto>> Handle(UpdateCatalogBrandCommand request, CancellationToken cancellationToken)
    {
        var catalogBrand = await context.CatalogBrands.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (catalogBrand == null)
        {
            return CatalogBrandErrors.NotFound(request.Id);
        }
        catalogBrand.Name = request.Name;
        await context.SaveChangesAsync(cancellationToken);
        return catalogBrand.Adapt<CatalogBrandDto>();
    }
}
