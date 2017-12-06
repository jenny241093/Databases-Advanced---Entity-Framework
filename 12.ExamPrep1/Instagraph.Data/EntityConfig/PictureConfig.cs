using System;
using System.Collections.Generic;
using System.Text;
using Instagraph.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Instagraph.Data.EntityConfig
{
    public class PictureConfig:IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Path)
                .IsRequired();

            builder.Property(p => p.Size)
                .IsRequired();

       

        }
    }
}
