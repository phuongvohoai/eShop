using Phuong.eShop.CatalogService.Application.CatalogProducts.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogProducts.Commands
{
    public record DeleteCatalogProductCommand(long Id) : IRequest<ApiResponse<bool>>;
    public class DeleteCatalogProductCommandHandler(ICatalogDbContext context) : IRequestHandler<DeleteCatalogProductCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(DeleteCatalogProductCommand command, CancellationToken cancellationToken)
        {
            var catalogProduct = await context.CatalogItems.FirstOrDefaultAsync(e => e.Id == command.Id, cancellationToken);
            if (catalogProduct == null)
            {
                return CatalogProductErrors.NotFound(command.Id);
            }
            context.CatalogItems.Remove(catalogProduct);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}