using System.Text.Json;
using Npgsql;
using Phuong.eShop.ServiceDefaults.Migration;

namespace Phuong.eShop.CatalogService.Infrastructure.Persistence;

public class CatalogSeed(IWebHostEnvironment env, ILogger<CatalogSeed> logger) : IDbSeeder<CatalogDbContext>
{
    private record CatalogSourceEntry(int Id, string Type, string Brand, string Name, string Description, decimal Price);

    public async Task SeedAsync(CatalogDbContext context, CancellationToken cancellationToken = default)
    {
        var contentRootPath = env.ContentRootPath;

        // Workaround from https://github.com/npgsql/efcore.pg/issues/292#issuecomment-388608426
        await context.Database.OpenConnectionAsync(cancellationToken);
        await ((NpgsqlConnection)context.Database.GetDbConnection()).ReloadTypesAsync();

        if (!context.CatalogItems.Any())
        {
            var sourceJson = await File.ReadAllTextAsync(Path.Combine(contentRootPath, "catalog.json"), cancellationToken);
            var sourceItems = JsonSerializer.Deserialize<CatalogSourceEntry[]>(sourceJson)!;

            SeedBrands(context, sourceItems);
            SeedTypes(context, sourceItems);

            await context.SaveChangesAsync(cancellationToken);

            var brandIdsByName = await context.CatalogBrands.ToDictionaryAsync(x => x.Name, x => x.Id, cancellationToken: cancellationToken);
            var typeIdsByName = await context.CatalogTypes.ToDictionaryAsync(x => x.Name, x => x.Id, cancellationToken: cancellationToken);

            SeedItems(context, sourceItems, brandIdsByName, typeIdsByName);

            await context.SaveChangesAsync(cancellationToken);
        }
    }

    private void SeedItems(CatalogDbContext context, CatalogSourceEntry[] sourceItems, Dictionary<string, long> brandIdsByName, Dictionary<string, long> typeIdsByName)
    {
        var catalogItems = sourceItems.Select(source => new CatalogItem
        {
            Name = source.Name,
            Description = source.Description,
            Price = source.Price,
            CatalogBrandId = brandIdsByName[source.Brand],
            CatalogTypeId = typeIdsByName[source.Type],
            AvailableStock = 100,
            PictureUri = $"{source.Id}.webp",
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "System",
        }).ToArray();
        context.CatalogItems.AddRange(catalogItems);
        logger.LogInformation("Seeded catalog with {NumItems} items", context.CatalogItems.Count());
    }

    private void SeedTypes(CatalogDbContext context, CatalogSourceEntry[] sourceItems)
    {
        context.CatalogTypes.RemoveRange(context.CatalogTypes);
        context.CatalogTypes.AddRange(
            sourceItems.Select(x => x.Type).Distinct().Select(typeName => new CatalogType
            {
                Name = typeName,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            })
        );
        logger.LogInformation("Seeded catalog with {NumTypes} types", context.CatalogTypes.Count());
    }

    private void SeedBrands(CatalogDbContext context, CatalogSourceEntry[] sourceItems)
    {
        context.CatalogBrands.RemoveRange(context.CatalogBrands);
        context.CatalogBrands.AddRange(
            sourceItems.Select(x => x.Brand).Distinct().Select(brandName => new CatalogBrand
            {
                Name = brandName,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            })

        );
        logger.LogInformation("Seeded catalog with {NumBrands} brands", context.CatalogBrands.Count());
    }
}