using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Enums;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class BankAccountConfiguration : DbEntityConfiguration<BankAccount>
    {
        public override void Configure(EntityTypeBuilder<BankAccount> entity)
        {
            entity.ToTable("BankAccounts");

            entity.HasOne(ba => ba.Company)
                .WithMany(c => c.BankAccounts)
                .HasForeignKey(ba => ba.CompanyId);

            entity.Property(ba => ba.Currency)
                .HasDefaultValue(Currency.BGN);
        }
    }
}
