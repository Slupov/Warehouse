using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class PlaceConfiguration : DbEntityConfiguration<Place>
    {
        public override void Configure(EntityTypeBuilder<Place> entity)
        {
            entity.ToTable("Places");

            //delete all products in the place after it was deleted
            entity.HasMany(x => x.Products)
                .WithOne(x => x.Place)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Company)
                .WithMany(x => x.Places);
        }
    }
}
