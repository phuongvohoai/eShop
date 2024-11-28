using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;

public record UpdateCatalogBranchCommand(string Name) : IRequest<ApiResponse<CatalogBrandDto>>
{
    public long? Id { get; set; }
}

public class UpdateCatalogBranchCommandHandler(ICatalogDbContext context) : IRequestHandler<UpdateCatalogBranchCommand, ApiResponse<CatalogBrandDto>>
{
    public async Task<ApiResponse<CatalogBrandDto>> Handle(UpdateCatalogBranchCommand request, CancellationToken cancellationToken)
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
