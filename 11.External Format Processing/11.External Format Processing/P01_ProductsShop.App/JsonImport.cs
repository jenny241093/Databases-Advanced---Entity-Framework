using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using P01_ProductsShop.Data;
using P01_ProductsShop.Models;

namespace P01_ProductsShop.App
{
   public class JsonImport
    {
      

        public JsonImport()
        {
            
        }
        public static void SetCategoriesFromJson()
        {
            using (var context=new ProductShopSystemContext())
            {
                var productIds = context.Products.Select(e=>e.Id).ToArray();
                var categoryIds = context.Categories.Select(e => e.Id).ToArray();

                Random rnd=new Random();
                var categoryProducts=new List<CategoryProduct>();
                foreach (var product in productIds)
                {
                    
                    for (int i = 0; i < 3; i++)
                    {
                        int categoryIndex = rnd.Next(0, categoryIds.Length);
                        while (categoryProducts.Any(cp=>cp.ProductId==product&&cp.CategoryId==categoryIds[categoryIndex]))
                        {
                            categoryIndex = rnd.Next(0, categoryIds.Length);
                        }
                        var catPr=new CategoryProduct()
                        {
                            ProductId = product,
                            CategoryId = categoryIds[categoryIndex]
                        };
                        categoryProducts.Add(catPr);
                    }
                }
                context.CategoryProducts.AddRange(categoryProducts);
                context.SaveChanges();
            }
        }

        public static string ImportProductsFromJson()
        {
            string path = "Files/products.json";
            Product[] products = ImportJson<Product>(path);
            Random rnd = new Random();

            using (var context = new ProductShopSystemContext())
            {
                int[] userIds = context.Users.Select(u => u.Id).ToArray();

                foreach (var p in products)
                {
                    int index = rnd.Next(0, userIds.Length);
                    int sellerId = userIds[index];
                    int? buyerId = sellerId;
                    while (buyerId == sellerId)
                    {
                        int buyerIndex = rnd.Next(0, userIds.Length);
                        buyerId = userIds[buyerIndex];
                    }
                    if (buyerId - sellerId < 5 && buyerId - sellerId > 0)
                    {
                        buyerId = null;
                    }
                    p.SellerId = sellerId;
                    p.BuyerId = buyerId;


                }
                context.Products.AddRange(products);
                context.SaveChanges();
            }
            return $"{products.Length} products were imported from file: {path}.";
        }

       public static string ImportCategoriesFromJson()
        {
            string path = "Files/categories.json";
            Category[] categories = ImportJson<Category>(path);
            using (var context = new ProductShopSystemContext())
            {
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }
            return $"{categories.Length} categories were imported from file: {path}.";
        }

       public static string ImportUsersFromJson()
        {
            string path = "Files/users.json";
            User[] users = ImportJson<User>(path);
            using (var context = new ProductShopSystemContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }
            return $"{users.Length} users were imported from file: {path}.";
        }

       public static T[] ImportJson<T>(string path)
        {
            string jsonString = File.ReadAllText(path);

            T[] objects = JsonConvert.DeserializeObject<T[]>(jsonString);
            return objects;
        }
    }
}