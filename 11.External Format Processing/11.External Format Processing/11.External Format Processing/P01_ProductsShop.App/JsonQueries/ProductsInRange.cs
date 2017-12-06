using System.IO;
using System.Linq;
using Newtonsoft.Json;
using P01_ProductsShop.Data;

namespace P01_ProductsShop.App.JsonQueries
{
    public class ProductsInRange
    {
        public static void GetproductsInRange()
        {
            using (ProductShopSystemContext db=new ProductShopSystemContext())
            {
                var products = db.Products.Where(p => p.Price >= 500 && p.Price <= 1000)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        name=p.Name,
                        price=p.Price,
                        seller = $"{p.Seller.FirstName} {p.Seller.LastName}"
                    }).ToArray();
                var jsonString = JsonConvert.SerializeObject(products,Formatting.Indented);
                File.WriteAllText("JsonExports/products-in-range.json", jsonString);
            }
        }
    }
}