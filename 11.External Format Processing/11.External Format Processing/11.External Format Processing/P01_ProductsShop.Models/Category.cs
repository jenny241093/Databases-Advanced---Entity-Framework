using System;
using System.Collections.Generic;
using System.Text;

namespace P01_ProductsShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryProduct> Products { get; set; }=new HashSet<CategoryProduct>();
    }
}
