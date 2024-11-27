using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;
namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands
{
    public record DeleteCatalogBrandCommand(long Id) : IRequest<ApiResponse<bool>>;
    public class DeleteCatalogBrandCommandHandler(ICatalogDbContext context) : IRequestHandler<DeleteCatalogBrandCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(DeleteCatalogBrandCommand request, CancellationToken cancellationToken)
        {
            var catalogBrand = await context.CatalogBrands.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (catalogBrand == null)
            {
                return CatalogBrandErrors.NotFound(request.Id);
            }
            context.CatalogBrands.Remove(catalogBrand);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

