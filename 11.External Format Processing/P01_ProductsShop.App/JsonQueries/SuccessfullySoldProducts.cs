using System.IO;
using System.Linq;
using Newtonsoft.Json;
using P01_ProductsShop.Data;

namespace P01_ProductsShop.App.JsonQueries
{
   public  class SuccessfullySoldProducts
    {
        public static void GetSuccessfullySoldProducts()
        {
            using (ProductShopSystemContext db = new ProductShopSystemContext())
            {
                var users = db.Users
                    .Where(e => e.ProductsSold.Count > 0&&e.ProductsSold.Any(p=>p.BuyerId!=null))
                    .OrderBy(e=>e.LastName)
                    .ThenBy(e => e.FirstName)
                    .Select(e => new
                    {
                        firstName=e.FirstName,
                        lastName=e.LastName,
                        soldProducts= e.ProductsSold.Select(p=>new
                        {
                            name=p.Name,
                            price=p.Price,
                            buyerFirstName=p.Buyer.FirstName,
                            buyerLastName=p.Buyer.LastName
                        }).ToArray()
                    }).ToArray();

                var jsonString = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings{DefaultValueHandling = DefaultValueHandling.Ignore});
                File.WriteAllText("JsonExports/users-sold-products.json", jsonString);
            }
        }
    }
}