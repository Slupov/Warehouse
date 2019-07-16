using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class PaymentConfiguration : DbEntityConfiguration<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> entity)
        {
            entity.ToTable("Payments");

            entity.HasOne(x => x.Company)
                .WithMany(x => x.Payments);
        }
    }
}
