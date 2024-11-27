using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;

public record UpdateCatalogBranchCommand(string Name) : IRequest<ApiResponse<CatalogBranchDto>>
{
    public long? Id { get; set; }
}

public class UpdateCatalogBranchCommandHandler(ICatalogDbContext context) : IRequestHandler<UpdateCatalogBranchCommand, ApiResponse<CatalogBranchDto>>
{
    public async Task<ApiResponse<CatalogBranchDto>> Handle(UpdateCatalogBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.CatalogBrands.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (entity == null)
        {
            return CatalogBrandErrors.NotFound(request.Id);
        }

        entity.Name = request.Name;
        await context.SaveChangesAsync(cancellationToken);

        return entity.Adapt<CatalogBranchDto>();
    }
}
