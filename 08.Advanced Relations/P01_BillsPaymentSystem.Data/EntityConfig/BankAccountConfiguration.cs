﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    class BankAccountConfiguration: IEntityTypeConfiguration<BankAccount>


    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(e => e.BankAccountId);

            builder.Property(e => e.Balance)
                .IsRequired();

            builder.Property(e => e.BankName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(e => e.SwiftCode)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(20);

            builder.Ignore(e => e.PaymentMethodId);
        }
    }
}
