using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;

public record CreateCatalogBrandCommand(string Name) : IRequest<ApiResponse<CatalogBrandDto>>;

public class CreateCatalogBrandCommandHandler(ICatalogDbContext context) : IRequestHandler<CreateCatalogBrandCommand, ApiResponse<CatalogBrandDto>>
{
    public async Task<ApiResponse<CatalogBrandDto>> Handle(CreateCatalogBrandCommand request, CancellationToken cancellationToken)
    {
        var entity = new CatalogBrand
        {
            Name = request.Name
        };
        context.CatalogBrands.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Adapt<CatalogBrandDto>();
    }
}