using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stations.Models;

namespace Stations.Data.EntityConfigurations
{
    public class TrainConfig:IEntityTypeConfiguration<Train>
    {
        public void Configure(EntityTypeBuilder<Train> builder)
        {
            builder.HasKey(e => e.Id);         
            builder.HasAlternateKey(e => e.TrainNumber);

            builder.HasMany(t => t.Trips)
                .WithOne(tr => tr.Train)
                .HasForeignKey(tr => tr.TrainId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
