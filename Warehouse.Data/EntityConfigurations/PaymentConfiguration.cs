using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class PaymentConfiguration : DbEntityConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.ToTable("Payments");

            entity.HasOne(x => x.Company)
                .WithMany(x => x.Payments);
        }
    }
}
