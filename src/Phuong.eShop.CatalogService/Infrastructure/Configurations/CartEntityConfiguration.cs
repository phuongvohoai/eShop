using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Phuong.eShop.CatalogService.Infrastructure.Configurations
{
    public class CartEntityConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
        }
    }
}
