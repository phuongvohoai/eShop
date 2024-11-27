namespace Phuong.eShop.CatalogService.Application.CatalogItems.Commands
{
    public class UpdateCatalogItemCommand : IRequest<long>
    {
        public required long Id {  get; set; } 
        public required string Name { get;set; }
        public required string PictureUri { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public required long CatalogTypeId { get; set; }
        public required long CatalogBrandId { get; set; }
        public int AvailableStock { get; set; }
    }
    public class UpdateCatalogItemCommandHandler(ICatalogDbContext context) : IRequestHandler<UpdateCatalogItemCommand, long>
    {
        public async Task<long> Handle(UpdateCatalogItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.CatalogItems.FindAsync(request.Id);
            if (entity == null) { return default; }
            entity.CatalogTypeId = request.CatalogTypeId;
            entity.CatalogBrandId = request.CatalogBrandId;
            entity.AvailableStock = request.AvailableStock;
            entity.Price = request.Price;
            entity.Description = request.Description;
            entity.Name = request.Name;
            entity.PictureUri = request.PictureUri;
            context.CatalogItems.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
