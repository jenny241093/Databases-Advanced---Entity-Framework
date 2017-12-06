using System;
using System.Collections;
using System.Collections.Generic;

namespace P01_ProductsShop.Models
{
    public class User
    {
        public int Id { get; set; }
        public int? Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Product> ProductsSold { get; set; }=new HashSet<Product>();
        public ICollection<Product> ProductsBought { get; set; }=new HashSet<Product>();
    }
}
