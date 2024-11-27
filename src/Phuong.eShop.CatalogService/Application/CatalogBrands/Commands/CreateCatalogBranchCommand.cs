using Mapster;
using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;

public record CreateCatalogBranchCommand(string Name) : IRequest<ApiResponse<CatalogBranchDto>>;

public class CreateCatalogBranchCommandHandler(ICatalogDbContext context) : IRequestHandler<CreateCatalogBranchCommand, ApiResponse<CatalogBranchDto>>
{
    public async Task<ApiResponse<CatalogBranchDto>> Handle(CreateCatalogBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = new CatalogBrand
        {
            Name = request.Name
        };
        context.CatalogBrands.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Adapt<CatalogBranchDto>();
    }
}