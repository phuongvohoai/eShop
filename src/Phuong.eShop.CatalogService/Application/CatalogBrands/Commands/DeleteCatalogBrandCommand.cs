using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands
{
    public record DeleteCatalogBrandCommand(long id) : IRequest<ApiResponse<bool>>;
    public class DeleteCatalogBrandCommandHandler(ICatalogDbContext context) : IRequestHandler<DeleteCatalogBrandCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(DeleteCatalogBrandCommand request, CancellationToken cancellationToken)
        {
            var value = context.CatalogBrands.FirstOrDefault(e => e.Id == request.id);
            if (value == null)
            {
                return CatalogBrandErrors.NotFound(request.id);
            }
            context.CatalogBrands.Remove(value);
            await context.SaveChangesAsync(cancellationToken);
            return true;

        }
    }

}

