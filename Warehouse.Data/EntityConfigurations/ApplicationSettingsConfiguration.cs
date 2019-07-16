using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class ApplicationSettingsConfiguration : DbEntityConfiguration<ApplicationSettings>
    {
        public override void Configure(EntityTypeBuilder<ApplicationSettings> entity)
        {
            entity.ToTable("ApplicationSettings");

            entity.HasOne(aps => aps.Company)
                .WithOne(c => c.ApplicationSettings);

            entity.Property(aps => aps.PricesIncludeVAT)
                .HasDefaultValue(true);

            entity.Property(aps => aps.PricesPrecisionUnits)
                .HasDefaultValue(2);
        }
    }
}
