using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.Property(e => e.FirstName)
                .IsUnicode()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.LastName)
                .IsUnicode()
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Email)
                .IsUnicode(false)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(e => e.Password)
                .IsUnicode()
                .HasMaxLength(25)
                .IsRequired();

          
        }
    }
}
