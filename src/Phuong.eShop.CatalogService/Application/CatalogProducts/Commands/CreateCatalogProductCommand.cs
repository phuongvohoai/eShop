using Phuong.eShop.CatalogService.Application.CatalogProducts.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogProducts.Commands
{
    public record CreateCatalogProductCommand(CatalogProductDto CatalogProduct) : IRequest<ApiResponse<CatalogProductDto>>;

    public class CreateCatalogProductCommandHandler(ICatalogDbContext context, IUserService userService) : IRequestHandler<CreateCatalogProductCommand, ApiResponse<CatalogProductDto>>
    {
        public async Task<ApiResponse<CatalogProductDto>> Handle(CreateCatalogProductCommand command, CancellationToken cancellationToken)
        {
            var catalogProduct = new CatalogItem
            {
                Name = command.CatalogProduct.Name,
                Description = command.CatalogProduct.Description,
                Price = command.CatalogProduct.Price,
                PictureUri = "",
                CatalogTypeId = command.CatalogProduct.CatalogTypeId,
                CatalogBrandId = command.CatalogProduct.CatalogBrandId,
                AvailableStock = command.CatalogProduct.AvailableStock,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userService.Name,
            };
            context.CatalogItems.Add(catalogProduct);
            await context.SaveChangesAsync(cancellationToken);

            //update url picture
            catalogProduct.PictureUri = $"api/files/{catalogProduct.Id}/content";
            await context.SaveChangesAsync(cancellationToken);

            return catalogProduct.Adapt<CatalogProductDto>();
        }
    }
}
