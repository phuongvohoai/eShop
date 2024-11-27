using Phuong.eShop.CatalogService.Application.CatalogTypes.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Commands
{
    public record DeleteCatalogTypeCommand(long id) : IRequest<ApiResponse<bool>>;
    public class DeleteCatalogTypeCommandHandler(ICatalogDbContext context) : IRequestHandler<DeleteCatalogTypeCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(DeleteCatalogTypeCommand request, CancellationToken cancellationToken)
        {
            var value = await context.CatalogTypes.FirstOrDefaultAsync(e => e.Id == request.id);
            if (value == null)
            {
                return CatalogTypeErrors.NotFound(request.id);
            }
            context.CatalogTypes.Remove(value);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}