using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.EntityConfigurations
{
  public class StationConfig:IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasKey(e => e.Id);     
            builder.HasAlternateKey(e => e.Name);

            builder.HasMany(s => s.TripsFrom)
                .WithOne(t => t.OriginStation)
                .HasForeignKey(s => s.OriginStationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.TripsTo)
                .WithOne(e => e.DestinationStation)
                .HasForeignKey(s => s.DestinationStationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
