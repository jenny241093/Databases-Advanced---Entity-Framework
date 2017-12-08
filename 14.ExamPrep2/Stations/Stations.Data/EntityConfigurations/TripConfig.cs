using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.EntityConfigurations
{
  public   class TripConfig:IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(t => t.Tickets)
                .WithOne(t => t.Trip)
                .HasForeignKey(t => t.TripId);
        }
    }
}
