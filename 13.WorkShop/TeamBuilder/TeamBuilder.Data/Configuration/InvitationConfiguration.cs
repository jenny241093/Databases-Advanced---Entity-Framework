using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    public class InvitationConfiguration:IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Team)
                .WithMany(t => t.Invitations)
                .HasForeignKey(e => e.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.InvitedUser)
                .WithMany(u => u.ReceivedInvitations)
                .HasForeignKey(e => e.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
