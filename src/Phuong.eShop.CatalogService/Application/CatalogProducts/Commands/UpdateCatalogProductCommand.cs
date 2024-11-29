using Phuong.eShop.CatalogService.Application.CatalogProducts.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogProducts.Commands
{
    public record UpdateCatalogProductCommand(CatalogProductDto CatalogProduct) : IRequest<ApiResponse<CatalogProductDto>>;
    public class UpdateCatalogProductCommandHandler(ICatalogDbContext context, IUserService userService) : IRequestHandler<UpdateCatalogProductCommand, ApiResponse<CatalogProductDto>>
    {
        public async Task<ApiResponse<CatalogProductDto>> Handle(UpdateCatalogProductCommand command, CancellationToken cancellationToken)
        {
            var catalogProduct = await context.CatalogItems.FirstOrDefaultAsync(e => e.Id == command.CatalogProduct.Id, cancellationToken);
            if (catalogProduct == null)
            {
                return CatalogProductErrors.NotFound(command.CatalogProduct.Id);
            };
            catalogProduct.Name = command.CatalogProduct.Name;
            catalogProduct.Description = command.CatalogProduct.Description;
            catalogProduct.Price = command.CatalogProduct.Price;
            catalogProduct.CatalogBrandId = command.CatalogProduct.CatalogBrandId;
            catalogProduct.CatalogTypeId = command.CatalogProduct.CatalogTypeId;
            catalogProduct.AvailableStock = command.CatalogProduct.AvailableStock;
            catalogProduct.ModifiedAt = DateTime.UtcNow;
            catalogProduct.ModifiedBy = userService.Name;
            await context.SaveChangesAsync(cancellationToken);
            return catalogProduct.Adapt<CatalogProductDto>();
        }
    }
}
