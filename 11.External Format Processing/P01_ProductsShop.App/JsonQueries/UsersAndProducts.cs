using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using P01_ProductsShop.Data;

namespace P01_ProductsShop.App.JsonQueries
{
   public class UsersAndProducts
    {
        public static void GetUsersWithAtLeastOneSoldProduct()
        {
            using (ProductShopSystemContext db = new ProductShopSystemContext())
            {
                var allProducts = db.Products
                    .Include(p => p.Seller)
                    .Where(p => p.BuyerId != null)
                    .ToArray();

                var sellersCount = allProducts.Select(p => p.Seller).Count();

                var users = new
                {
                    userCount = sellersCount,
                    users = allProducts.Select(p => new
                        {
                            firstName=p.Seller.FirstName,
                            lastName=p.Seller.LastName,
                            age=p.Seller.Age,
                            soldProducts = new
                            {
                                count=p.Seller.ProductsSold.Count,
                                products=p.Seller.ProductsSold.Select(ps=>new
                                {
                                    name=ps.Name,
                                    price=ps.Price
                                }).ToArray()
                            }
                        }).ToArray()
                        .OrderByDescending(p=>p.soldProducts.count)
                        .ThenBy(p=>p.lastName)
                };

                var jsonString = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
                File.WriteAllText("JsonExports/users-and-products.json", jsonString);
            }
            
          
        }
    }
}