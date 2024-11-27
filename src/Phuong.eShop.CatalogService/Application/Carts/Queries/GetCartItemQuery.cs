namespace Phuong.eShop.CatalogService.Application.Carts.Queries
{
    public class GetCartItemsQuery : IRequest<CartItemDto?>
    {
        public required int UserId { get; set; }
    }

    public class GetCartItemsQueryHandler(ICatalogDbContext context) : IRequestHandler<GetCartItemsQuery, CartItemDto?>
    {
        public async Task<CartItemDto?> Handle(GetCartItemsQuery request, CancellationToken cancellationToken)
        {
            return await context.Carts
                .AsNoTracking()
                .Where(e => e.Id == request.UserId)
                .ProjectToType<CartItemDto>()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
