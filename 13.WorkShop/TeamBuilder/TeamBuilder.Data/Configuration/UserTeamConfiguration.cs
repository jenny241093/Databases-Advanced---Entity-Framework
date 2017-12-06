using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class UserTeamConfiguration:IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.HasKey(et => new {et.UserId, et.TeamId});

            builder.HasOne(ut => ut.User)
                .WithMany(u => u.UserTeams)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(et => et.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(et => et.TeamId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
