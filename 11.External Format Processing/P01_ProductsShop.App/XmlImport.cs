using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using P01_ProductsShop.Data;
using P01_ProductsShop.Models;

namespace P01_ProductsShop.App
{
    public class XmlImport
    {
        public static string ImportUsersFromXml()
        {
            string path = "Files/users.xml";
            string xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);
            var elements = xmlDoc.Root.Elements();
            var users = new List<User>();
            foreach (var e in elements)
            {
                string firstName = e.Attribute("firstName")?.Value;
                string lastName = e.Attribute("lastName")?.Value;
                int? age = null;
                if (e.Attribute("age") != null)
                {
                    age = int.Parse(e.Attribute("age").Value);
                }
                var user = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };
                users.Add(user);
            }
            using (var context = new ProductShopSystemContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
                return $"{users.Count} users were imported from file: {path}.";
            }
        }

        public static string ImportCategoriesFromXml()
        {
            string path = "Files/categories.xml";
            string xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);
            var elements = xmlDoc.Root.Elements();
            var categories = new List<Category>();
            foreach (var e in elements)
            {
                string name = e.Element("name")?.Value;

                var category = new Category()
                {
                    Name = name
                };
                categories.Add(category);
            }
            using (var context = new ProductShopSystemContext())
            {
                context.Categories.AddRange(categories);
                context.SaveChanges();
                return $"{categories.Count} categories were imported from file: {path}.";
            }


        }


        public static string ImportProductsFromXml()
        {
            string path = "Files/products.xml";
            string xmlString = File.ReadAllText(path);
            var xmlDoc = XDocument.Parse(xmlString);
            var elements = xmlDoc.Root.Elements();
            var products = new List<Product>();
            Random rnd = new Random();
            using (var context = new ProductShopSystemContext())
            {

                var userIds = context.Users.Select(u => u.Id).ToArray();
                //var categoryIds = context.Categories.Select(c => c.Id).ToArray();

                foreach (var e in elements)
                {
                    var index = rnd.Next(0, userIds.Length);
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

                    string name = e.Element("name")?.Value;
                    decimal price = decimal.Parse(e.Element("price").Value);

                    var product = new Product()
                    {
                        Name = name,
                        Price = price,
                        SellerId = sellerId,
                        BuyerId = buyerId
                    };
                    products.Add(product);
                }
                context.Products.AddRange(products);
                context.SaveChanges();
                return $"{products.Count} products were imported from file: {path}.";
            }
        }
        public static void SetCategoriesFromXml()
        {
            using (var context = new ProductShopSystemContext())
            {
                var productIds = context.Products.Select(e => e.Id).ToArray();
                var categoryIds = context.Categories.Select(e => e.Id).ToArray();

                Random rnd = new Random();
                var categoryProducts = new List<CategoryProduct>();
                foreach (var product in productIds)
                {

                    for (int i = 0; i < 3; i++)
                    {
                        int categoryIndex = rnd.Next(0, categoryIds.Length);
                        while (categoryProducts.Any(cp => cp.ProductId == product && cp.CategoryId == categoryIds[categoryIndex]))
                        {
                            categoryIndex = rnd.Next(0, categoryIds.Length);
                        }
                        var catPr = new CategoryProduct()
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
    }
}

