using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Enums;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class SubscriptionConfiguration : DbEntityConfiguration<Subscription>
    {
        public override void Configure(EntityTypeBuilder<Subscription> entity)
        {
            entity.ToTable("Subscriptions");

            entity.HasOne(x => x.Company)
                .WithMany(x => x.Subscriptions);

            entity.Property(x => x.PaymentStatus)
                .HasDefaultValue(PaymentStatus.NOT_PAYED);
        }
    }
}