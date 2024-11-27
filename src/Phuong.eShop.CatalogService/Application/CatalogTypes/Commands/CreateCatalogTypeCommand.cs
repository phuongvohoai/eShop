using Phuong.eShop.CatalogService.Application.CatalogTypes.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Commands
{
    public record CreateCatalogTypeCommand(string Name) : IRequest<ApiResponse<CatalogTypeDto>>;

    public class CreateCatalogTypeCommandHandler(ICatalogDbContext context) : IRequestHandler<CreateCatalogTypeCommand, ApiResponse<CatalogTypeDto>>
    {
        public async Task<ApiResponse<CatalogTypeDto>> Handle (CreateCatalogTypeCommand request, CancellationToken cancellationToken)
        {
            var value = new CatalogType
            {
                Name = request.Name,
            };
            context.CatalogTypes.Add(value);
            await context.SaveChangesAsync(cancellationToken);
            return value.Adapt<CatalogTypeDto>(); 
        }
    }
}
