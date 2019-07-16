using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Enums;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class VoucherConfiguration : DbEntityConfiguration<Voucher>
    {
        public override void Configure(EntityTypeBuilder<Voucher> entity)
        {
            entity.ToTable("Vouchers");

            entity.Property(x => x.VoucherType)
                .HasDefaultValue(VoucherType.UNDEFINED);
        }
    }
}
