namespace Phuong.eShop.CatalogService.Application.Common;

public static class QueryableExtensions
{
    public static async Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default) where TDestination : class
    {
        var count = await queryable.CountAsync(cancellationToken);
        var items = await queryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return new PaginatedList<TDestination>(items, count, pageNumber, pageSize);
    }

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
        this IQueryable queryable,
        CancellationToken cancellationToken = default) where TDestination : class
        => queryable.ProjectToType<TDestination>().AsNoTracking().ToListAsync(cancellationToken);
}