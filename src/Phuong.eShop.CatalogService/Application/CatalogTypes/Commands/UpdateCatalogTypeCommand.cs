using Phuong.eShop.CatalogService.Application.CatalogBrands.Models;
using System.Threading;
using Phuong.eShop.CatalogService.Application.CatalogTypes.Models;
using static Grpc.Core.Metadata;

namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Commands
{

    public record UpdateCatalogTypeCommand(long Id, string Name) : IRequest<ApiResponse<CatalogTypeDto>>;
    public class UpdateCatalogTypeCommandHandler(ICatalogDbContext context) : IRequestHandler<UpdateCatalogTypeCommand, ApiResponse<CatalogTypeDto>>
    {
        public async Task<ApiResponse<CatalogTypeDto>> Handle (UpdateCatalogTypeCommand request, CancellationToken cancellationToken)
        {
            var value =  await context.CatalogTypes.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (value == null)
            {
                return CatalogTypeErrors.NotFound(request.Id);
            }
            value.Name = request.Name;
            await context.SaveChangesAsync(cancellationToken);
            return value.Adapt<CatalogTypeDto>();
        }
    }
}
