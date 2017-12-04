using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_ProductsShop.Models;

namespace P01_ProductsShop.Data.EntityConfiguration
{
   public class CategoryProductConfiguration:IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasKey(e => new {e.CategoryId, e.ProductId});
        }
    }
}
