using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.EntityConfigurations
{
  public  class CustomerCardConfig:IEntityTypeConfiguration<CustomerCard>
    {
        public void Configure(EntityTypeBuilder<CustomerCard> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(128);
            builder.Property(e => e.Age).IsRequired();

            builder.HasMany(cc => cc.BoughtTickets)
                .WithOne(t => t.CustomerCard)
                .HasForeignKey(t => t.CustomerCardId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
