using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Phuong.eShop.CatalogService.Infrastructure.Configurations;

public class CatalogBranchEntityConfiguration : IEntityTypeConfiguration<CatalogBrand>
{
    public void Configure(EntityTypeBuilder<CatalogBrand> builder)
    {
        builder.HasKey(p => p.Id);
    }
}