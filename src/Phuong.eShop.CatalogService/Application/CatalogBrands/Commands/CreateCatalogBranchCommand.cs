namespace Phuong.eShop.CatalogService.Application.CatalogBrands.Commands;

public record CreateCatalogBranchCommand(string Name) : IRequest<long>;

public class CreateCatalogBranchCommandHandler(ICatalogDbContext context) : IRequestHandler<CreateCatalogBranchCommand, long>
{
    public async Task<long> Handle(CreateCatalogBranchCommand request, CancellationToken cancellationToken)
    {
        var entity = new CatalogBrand
        {
            Name = request.Name
        };
        context.CatalogBrands.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}