using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Warehouse.Data.Models;

using Warehouse.Data.EntityConfigurations;
using Warehouse.Data.EntityConfigurations.Extensions;

namespace Warehouse.Data
{
    public class WarehouseDbContext : IdentityDbContext<ApplicationUser>
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationSettings> ApplicationSettings { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<IncomeExpense> IncomeExpenses { get; set; }
        public DbSet<MeasureUnit> MeasureUnits { get; set; }
        public DbSet<Partner> Partners { get; set; }
        public DbSet<Order> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.AddConfiguration(new ApplicationSettingsConfiguration());
            builder.AddConfiguration(new BankAccountConfiguration());
            builder.AddConfiguration(new CompanyConfiguration());
            builder.AddConfiguration(new ContactsConfiguration());
            builder.AddConfiguration(new IncomeExpenseConfiguration());
            builder.AddConfiguration(new MeasureUnitConfiguration());
            builder.AddConfiguration(new PartnerConfiguration());
            builder.AddConfiguration(new PaymentConfiguration());
            builder.AddConfiguration(new PlaceConfiguration());
            builder.AddConfiguration(new ProductConfiguration());
            builder.AddConfiguration(new SubscriptionConfiguration());
            builder.AddConfiguration(new VoucherConfiguration());
        }
    }
}
