
namespace Phuong.eShop.CatalogService.Application.Carts.Commands;

public class CreateCartCommand : IRequest<long>
{
    public required string Data { get; set; }
    public required long UserId { get; set; }
}

public class CreateCartCommandHandler(ICatalogDbContext context) : IRequestHandler<CreateCartCommand, long>
{
    public async Task<long> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var exitsingCart = await context.Carts.Where(e => e.Id == request.UserId).FirstOrDefaultAsync(cancellationToken);
        if (exitsingCart != null)
        {
            exitsingCart.Data = request.Data;
        }
        else
        {
            exitsingCart = new Cart
            {
                Data = request.Data,
                Id = request.UserId,
            };
            context.Carts.Add(exitsingCart);
        }
        await context.SaveChangesAsync(cancellationToken);
        return exitsingCart.Id;
    }
}

