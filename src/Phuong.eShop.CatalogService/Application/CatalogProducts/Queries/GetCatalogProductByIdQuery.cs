using Phuong.eShop.CatalogService.Application.CatalogProducts.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogProducts.Queries
{
    public record GetCatalogProductByIdQuery(long Id) : IRequest<ApiResponse<CatalogProductDto>>;
    public class GetCatalogProductByIdQueryHandler(ICatalogDbContext context) : IRequestHandler<GetCatalogProductByIdQuery, ApiResponse<CatalogProductDto>>
    {
        public async Task<ApiResponse<CatalogProductDto>> Handle(GetCatalogProductByIdQuery request, CancellationToken cancellationToken)
        {
            var catalogProduct = await context.CatalogItems.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (catalogProduct == null)
            {
                CatalogProductErrors.NotFound(request.Id);
            }
            return catalogProduct.Adapt<CatalogProductDto>();
        }
    }
}
