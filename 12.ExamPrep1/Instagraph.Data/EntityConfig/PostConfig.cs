using System;
using System.Collections.Generic;
using System.Text;
using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.EntityConfig
{
   public  class PostConfig:IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(e => e.Caption).IsRequired();

            builder.Property(e => e.PictureId).IsRequired();

            builder.HasOne(e => e.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Picture)
                .WithMany(p => p.Posts)
                .HasForeignKey(e => e.PictureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
