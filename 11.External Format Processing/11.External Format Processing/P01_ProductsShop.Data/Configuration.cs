using System;

namespace P01_ProductsShop.Data
{
    public class Configuration  
    {
        public static string ConnectionString { get; set; } =
            @"Server=DESKTOP-5CTIM8C\SQLEXPRESS;Database=ProductShop;Integrated Security=True";
    }
}
