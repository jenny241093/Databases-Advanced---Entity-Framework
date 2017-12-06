using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class EventTeamConfiguration:IEntityTypeConfiguration<EventTeam>
    {
        public void Configure(EntityTypeBuilder<EventTeam> builder)
        {
            builder.ToTable("EventTeams");

            builder.HasKey(et => new {et.EventId, et.TeamId});

            builder.HasAlternateKey(et => new {et.TeamId, et.EventId});

            builder.HasOne(et => et.Event)
                .WithMany(e => e.ParticipatingEventTeams)
                .HasForeignKey(et => et.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(et => et.Team)
                .WithMany(t => t.ParticipatedEvents)
                .HasForeignKey(et => et.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
