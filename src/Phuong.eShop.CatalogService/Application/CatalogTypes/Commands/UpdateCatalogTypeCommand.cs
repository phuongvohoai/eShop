using Phuong.eShop.CatalogService.Application.CatalogTypes.Models;
namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Commands
{
    public record UpdateCatalogTypeCommand(long Id, string Name) : IRequest<ApiResponse<CatalogTypeDto>>;
    public class UpdateCatalogTypeCommandHandler(ICatalogDbContext context) : IRequestHandler<UpdateCatalogTypeCommand, ApiResponse<CatalogTypeDto>>
    {
        public async Task<ApiResponse<CatalogTypeDto>> Handle(UpdateCatalogTypeCommand request, CancellationToken cancellationToken)
        {
            var catalogType = await context.CatalogTypes.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (catalogType == null)
            {
                return CatalogTypeErrors.NotFound(request.Id);
            }
            catalogType.Name = request.Name;
            await context.SaveChangesAsync(cancellationToken);
            return catalogType.Adapt<CatalogTypeDto>();
        }
    }
}
