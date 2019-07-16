using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class IncomeExpenseConfiguration : DbEntityConfiguration<IncomeExpense>
    {
        public override void Configure(EntityTypeBuilder<IncomeExpense> entity)
        {
            entity.ToTable("IncomeExpenses");

            entity.HasOne(x => x.Company)
                .WithMany(x => x.IncomeExpenses);
        }
    }
}
