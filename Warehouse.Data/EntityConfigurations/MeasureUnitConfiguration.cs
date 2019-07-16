using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class MeasureUnitConfiguration : DbEntityConfiguration<MeasureUnit>
    {
        public override void Configure(EntityTypeBuilder<MeasureUnit> entity)
        {
            entity.ToTable("MesureUnits");

            entity.HasOne(x => x.Company)
                .WithMany(x => x.MeasureUnits);

            entity.HasMany(x => x.Products)
                .WithOne(x => x.MeasureUnit);
        }
    }
}
