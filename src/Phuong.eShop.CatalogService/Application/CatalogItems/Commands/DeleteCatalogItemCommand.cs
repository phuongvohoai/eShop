using Microsoft.EntityFrameworkCore;
using Phuong.eShop.CatalogService.Application.Entities;

namespace Phuong.eShop.CatalogService.Application.CatalogItems.Commands
{
    public class DeleteCatalogItemCommand : IRequest<long>
    {
        public DeleteCatalogItemCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }
    public class DeleteCatalogItemCommandHandler(ICatalogDbContext context) : IRequestHandler<DeleteCatalogItemCommand, long>
    {
        public async Task<long> Handle (DeleteCatalogItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await context.CatalogItems.FindAsync(request.Id);
            if (entity is null)
            {
                return 0;
            }
            context.CatalogItems.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
