using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class ProductGroupConfiguration : DbEntityConfiguration<ProductGroup>
    {
        public override void Configure(EntityTypeBuilder<ProductGroup> entity)
        {
            entity.ToTable("ProductGroups");

            entity.HasOne(x => x.Company)
                .WithMany(x => x.ProductGroups)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(x => x.Products)
                .WithOne(x => x.ProductGroup)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
