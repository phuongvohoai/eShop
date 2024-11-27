using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;

public record UpdateCatalogBrandCommand(string Name) : IRequest<ApiResponse<CatalogBrandDto>>
{
    public long? Id { get; set; }
}

public class UpdateCatalogBrandCommandHandler(ICatalogDbContext context) : IRequestHandler<UpdateCatalogBrandCommand, ApiResponse<CatalogBrandDto>>
{
    public async Task<ApiResponse<CatalogBrandDto>> Handle(UpdateCatalogBrandCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.CatalogBrands.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return CatalogBrandErrors.NotFound(request.Id);
        }

        entity.Name = request.Name;
        await context.SaveChangesAsync(cancellationToken);

        return entity.Adapt<CatalogBrandDto>();
    }
}
