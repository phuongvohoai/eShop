using Phuong.eShop.CatalogService.Application.CatalogTypes.Models;
namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Commands
{
    public record DeleteCatalogTypeCommand(long Id) : IRequest<ApiResponse<bool>>;
    public class DeleteCatalogTypeCommandHandler(ICatalogDbContext context) : IRequestHandler<DeleteCatalogTypeCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(DeleteCatalogTypeCommand request, CancellationToken cancellationToken)
        {
            var catalogType = await context.CatalogTypes.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (catalogType == null)
            {
                return CatalogTypeErrors.NotFound(request.Id);
            }
            context.CatalogTypes.Remove(catalogType);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}