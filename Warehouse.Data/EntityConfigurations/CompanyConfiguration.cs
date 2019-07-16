using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class CompanyConfiguration : DbEntityConfiguration<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> entity)
        {
            entity.ToTable("Companies");

            // company - contacts
            entity.HasOne(x => x.Contacts)
                .WithOne(x => x.Company)
                .HasForeignKey(typeof(Contacts));

            // company - products*
            entity.HasMany(x => x.Products)
                .WithOne(x => x.Company);

            // company - bankaccounts*
            entity.HasMany(x => x.BankAccounts)
                .WithOne(x => x.Company);

            // company - productgroups*
            entity.HasMany(x => x.ProductGroups)
                .WithOne(x => x.Company);

            // company - measureunits*
            entity.HasMany(x => x.MeasureUnits)
                .WithOne(x => x.Company);

            // company - applicationsUsers*
            entity.HasMany(x => x.ApplicationUsers)
                .WithOne(x => x.Company);

            // company - places*
            entity.HasMany(x => x.Places)
                .WithOne(x => x.Company);

            // company - applicationsettings*
            entity.HasOne(x => x.ApplicationSettings)
                .WithOne(x => x.Company)
                .HasForeignKey(typeof(ApplicationSettings));

            // company - payments*
            entity.HasMany(x => x.Payments)
                .WithOne(x => x.Company);
        }
    }
}
