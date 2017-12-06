using System;
using System.Collections.Generic;
using System.Text;
using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.EntityConfig
{
  public   class UserConfig:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasAlternateKey(u => u.Username);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(30);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(20);
            builder.Property(e => e.ProfilePictureId).IsRequired();

            builder.HasOne(e => e.ProfilePicture)
                .WithMany(p => p.Users)
                .HasForeignKey(e => e.ProfilePictureId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
