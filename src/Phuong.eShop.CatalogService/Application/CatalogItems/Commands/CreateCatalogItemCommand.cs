using System.Reflection.Metadata;
using Phuong.eShop.CatalogService.Application.Carts.Commands;

namespace Phuong.eShop.CatalogService.Application.CatalogItems.Commands
{
    public class CreateCatalogItemCommand : IRequest<long>
    {
        public required string Name { get; set; }
        public required string PictureUri { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public required long CatalogTypeId { get; set; }
        public required long CatalogBrandId { get; set; }
        public int AvailableStock { get; set; }
    }
    public class CreateCatalogItemCommandHandler(ICatalogDbContext context) : IRequestHandler<CreateCatalogItemCommand, long>
    {
        public async Task<long> Handle(CreateCatalogItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new CatalogItem
            {
                Name = request.Name,
                PictureUri = request.PictureUri,
                Description = request.Description,
                Price = request.Price,
                CatalogTypeId = request.CatalogTypeId,
                AvailableStock = request.AvailableStock,
                CatalogBrandId = request.CatalogBrandId,
            };
            context.CatalogItems.Add(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

    }
}
