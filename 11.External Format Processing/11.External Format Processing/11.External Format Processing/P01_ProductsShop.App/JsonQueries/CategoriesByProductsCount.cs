using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using P01_ProductsShop.Data;

namespace P01_ProductsShop.App.JsonQueries
{
   public  class CategoriesByProductsCount
    {
        public  static void GetCategoriesByProductsCount()
        {
            using (ProductShopSystemContext db = new ProductShopSystemContext())
            {
                var categories = db.Categories
                    .Include(p => p.Products)            
                    .Select(c => new
                    {
                        category=c.Name,
                        productsCount=c.Products.Count,
                        averagePrice=c.Products.Average(p=>p.Product.Price),
                        totalRevenue=c.Products.Sum(p=>p.Product.Price)
                    })
                    .OrderBy(a=>a.category)
                    .ToArray();

                var jsonString = JsonConvert.SerializeObject(categories, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
                File.WriteAllText("JsonExports/categories-by-products.json", jsonString);

            }
        }
    }
}