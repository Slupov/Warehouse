using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class PartnerConfiguration : DbEntityConfiguration<Partner>
    {
        public override void Configure(EntityTypeBuilder<Partner> entity)
        {
            entity.ToTable("Partners");

            entity.HasOne(x => x.Contacts);
        }
    }
}
