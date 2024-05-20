using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Phuong.eShop.CatalogService.Infrastructure.Configurations;

public class CatalogItemEntityConfiguration : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired();
        builder.Property(s => s.PictureUri).IsRequired();
        builder.Property(s => s.Price).HasColumnType("decimal(18,2)");
        builder.Property(s => s.CatalogTypeId).IsRequired();
        builder.Property(s => s.CatalogBrandId).IsRequired();
        builder.Property(s => s.AvailableStock).IsRequired();
        builder.Property(s => s.Description).IsRequired(false);

        builder.HasOne(s => s.CatalogBrand).WithMany();
        builder.HasOne(s => s.CatalogType).WithMany();

        builder.HasIndex(s => s.Name);
    }
}