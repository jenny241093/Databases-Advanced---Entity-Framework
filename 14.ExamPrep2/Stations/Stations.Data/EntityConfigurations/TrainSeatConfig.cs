using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.EntityConfigurations
{
    public class TrainSeatConfig:IEntityTypeConfiguration<TrainSeat>
    {
        public void Configure(EntityTypeBuilder<TrainSeat> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Quantity).IsRequired();


            builder.HasOne(e => e.Train)
                .WithMany(s => s.TrainSeats)
                .HasForeignKey(e => e.TrainId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ts => ts.SeatingClass)
                .WithMany(sc => sc.TrainSeats)
                .HasForeignKey(ts => ts.SeatingClassId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
