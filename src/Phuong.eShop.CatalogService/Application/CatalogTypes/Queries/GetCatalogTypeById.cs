using Phuong.eShop.CatalogService.Application.CatalogTypes.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Queries
{
    public record GetCatalogTypeById(long id) : IRequest<ApiResponse<CatalogTypeDto>>;
    public class GetCatalogTypeByIdHandler(ICatalogDbContext context) : IRequestHandler<GetCatalogTypeById, ApiResponse<CatalogTypeDto>>
    {
        public async Task<ApiResponse<CatalogTypeDto>> Handle(GetCatalogTypeById request, CancellationToken cancellation)
        {
            var value = await context.CatalogTypes.FirstOrDefaultAsync(e => e.Id == request.id);
            if (value == null)
            {
                return CatalogTypeErrors.NotFound(request.id);
            }
            return value.Adapt<CatalogTypeDto>();
        }
    }
}

