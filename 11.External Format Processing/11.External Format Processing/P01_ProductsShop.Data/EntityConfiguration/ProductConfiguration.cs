using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_ProductsShop.Models;

namespace P01_ProductsShop.Data.EntityConfiguration
{
public class ProductConfiguration:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .IsUnicode();

            builder.Property(e => e.BuyerId)
                .IsRequired(false);

            builder.HasOne(p => p.Seller)
                .WithMany(s => s.ProductsSold)
                .HasForeignKey(p => p.SellerId);

            builder.HasOne(p => p.Buyer)
                .WithMany(e => e.ProductsBought)
                .HasForeignKey(p => p.BuyerId);
        }
    }
}
