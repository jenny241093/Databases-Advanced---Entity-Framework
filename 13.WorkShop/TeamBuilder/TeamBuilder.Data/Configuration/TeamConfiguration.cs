using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class TeamConfiguration:IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.Name).IsUnique();

            builder.Property(t => t.Name)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(32);

            builder.Property(t => t.Acronym)
                .IsRequired()
                .HasColumnType("NVARCHAR(3)");

            builder.HasOne(t => t.Creator)
                .WithMany(u => u.CreatedUserTeams)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Invitations)
                .WithOne(i => i.Team)
                .HasForeignKey(i => i.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
