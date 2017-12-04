using System;
using System.Collections.Generic;
using System.Text;

namespace P01_ProductsShop.Models
{
   public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int SellerId { get; set; }
        public User Seller { get; set; }

        public int? BuyerId { get; set; }
        public User Buyer { get; set; }

        public ICollection<CategoryProduct> Categories { get; set; }=new HashSet<CategoryProduct>();
    }
}
