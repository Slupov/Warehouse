using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Data.EntityConfigurations.Extensions;
using Warehouse.Data.Models;

namespace Warehouse.Data.EntityConfigurations
{
    public class ContactsConfiguration : DbEntityConfiguration<Contacts>
    {
        public override void Configure(EntityTypeBuilder<Contacts> entity)
        {
            entity.ToTable("Contacts");

            entity.HasOne(x => x.Company)
                .WithOne(x => x.Contacts);
        }
    }
}
