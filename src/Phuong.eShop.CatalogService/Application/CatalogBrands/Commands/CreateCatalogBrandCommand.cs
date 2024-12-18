using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;

public record CreateCatalogBrandCommand(string Name) : IRequest<ApiResponse<CatalogBrandDto>>;

public class CreateCatalogBrandCommandHandler(ICatalogDbContext context, IUserService userService) : IRequestHandler<CreateCatalogBrandCommand, ApiResponse<CatalogBrandDto>>
{
    public async Task<ApiResponse<CatalogBrandDto>> Handle(CreateCatalogBrandCommand request, CancellationToken cancellationToken)
    {
        var catalogBrand = new CatalogBrand
        {
            Name = request.Name,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userService.Name
        };
        context.CatalogBrands.Add(catalogBrand);
        await context.SaveChangesAsync(cancellationToken);
        return catalogBrand.Adapt<CatalogBrandDto>();
    }
}