using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class ProductConfiguration : DbEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.ToTable("Products");

            entity.HasOne(x => x.Company)
                .WithMany(x => x.Products);

            entity.HasOne(x => x.Place)
                .WithMany(x => x.Products)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.MeasureUnit)
                .WithMany(x => x.Products)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.ProductGroup)
                .WithMany(x => x.Products)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
