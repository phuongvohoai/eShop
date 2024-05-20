using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Phuong.eShop.CatalogService.Infrastructure.Configurations;

public class CatalogTypeEntityConfiguration : IEntityTypeConfiguration<CatalogType>
{
    public void Configure(EntityTypeBuilder<CatalogType> builder)
    {
        builder.HasKey(p => p.Id);
    }
}