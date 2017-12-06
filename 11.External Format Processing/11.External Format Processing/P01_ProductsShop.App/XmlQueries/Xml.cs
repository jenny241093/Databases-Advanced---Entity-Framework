using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using P01_ProductsShop.Data;

namespace P01_ProductsShop.App.XmlQueries
{
    public class Xml
    {

        public static void ProductsInRange(ProductShopSystemContext context)
        {
            using (context)
            {
                var products = context.Products.Where(p => p.Price >= 1000 && p.Price <= 2000 && p.Buyer != null)
                    .Select(p => new
                    {
                        productName = p.Name,
                        price = p.Price,
                        buyer = $"{p.Buyer.FirstName} {p.Buyer.LastName}"
                    }).ToArray();
                var xDoc = new XDocument();
                xDoc.Add(new XElement("products"));
                foreach (var p in products)
                {
                    xDoc.Root.Add(
                        new XElement("product",
                            new XAttribute("name", $"{p.productName}"),
                            new XAttribute("price", $"{p.price}"),
                            new XAttribute("buyer", $"{p.buyer}")));
                }
                //xDoc.Save("XmlExports/products-in-range.xml");
                string xmlString = xDoc.ToString();
                File.WriteAllText("XmlExports/products-in-range.xml", xmlString);
            }
        }

        public static void SoldProducts(ProductShopSystemContext context)
        {
            using (context)
            {
                var users = context.Users
                    .Where(e => e.ProductsSold.Count > 0 && e.ProductsSold.Any(p => p.BuyerId != null))
                    .OrderBy(e => e.LastName)
                    .ThenBy(e => e.FirstName)
                    .Select(e => new
                    {
                        firstName = e.FirstName,
                        lastName = e.LastName,
                        soldProducts = e.ProductsSold.Select(p => new
                        {
                            name = p.Name,
                            price = p.Price,
                            buyerFirstName = p.Buyer.FirstName,
                            buyerLastName = p.Buyer.LastName
                        }).ToArray()
                    }).ToArray();
                var xDoc = new XDocument();
                XElement userListXml = new XElement("users");
                foreach (var user in users)
                {
                    XElement userXml = new XElement("user");
                    if (user.firstName != null)
                    {
                        userXml.SetAttributeValue("first-name", user.firstName);
                    }
                    userXml.SetAttributeValue("last-name", user.lastName);
                    XElement productListXml = new XElement("sold-producst");
                    var products = user.soldProducts;
                    foreach (var product in products)
                    {
                        XElement productXml = new XElement("product");
                        productXml.SetElementValue("name", product.name);
                        productXml.SetElementValue("price", product.price);
                        productListXml.Add(productXml);
                    }

                    userXml.Add(productListXml);
                    userListXml.Add(userXml);
                }
                xDoc.Add(userListXml);
                //xDoc.Save("XmlExports/products-in-range.xml");
                string xmlString = xDoc.ToString();
                File.WriteAllText("XmlExports/users-sold-products.xml", xmlString);
            }
        }

        public static void CategoriesByeProductsCount(ProductShopSystemContext context)
        {
            using (context)
            {
                var categories = context.Categories
                    .Include(p => p.Products)
                    .Select(c => new
                    {
                        category = c.Name,
                        productsCount = c.Products.Count,
                        averagePrice = c.Products.Average(p => p.Product.Price),
                        totalRevenue = c.Products.Sum(p => p.Product.Price)
                    })
                    .OrderBy(a => a.category)
                    .ToArray();
                XDocument xDocument = new XDocument();

                XElement categoryListXml = new XElement("categories");

                foreach (var category in categories)
                {
                    XElement categoryXml = new XElement("category");

                    categoryXml.SetAttributeValue("name", category.category);
                    categoryXml.SetElementValue("product-count", category.productsCount);
                    categoryXml.SetElementValue("average-price", category.averagePrice);
                    categoryXml.SetElementValue("total-revenue", category.totalRevenue);

                    categoryListXml.Add(categoryXml);
                }

                xDocument.Add(categoryListXml);
                xDocument.Save("XmlExports/categories-by-products.xml");
            }
        }

        public static void UsersAndProducts(ProductShopSystemContext db)
        {
            using (db)
            {
                var users = db.Users
                    .Include(u => u.ProductsSold)
                    .Where(u => u.ProductsSold.Count > 0)
                    .OrderByDescending(u => u.ProductsSold.Count)
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        firstName = u.FirstName,
                        lastName = u.LastName,
                        age = u.Age,
                        soldProducts = new
                        {
                            count = u.ProductsSold.Count,
                            products = u.ProductsSold.Select(p => new
                            {
                                productName = p.Name,
                                prductPrice = p.Price
                            })
                        }
                    });

              

                XDocument xDocument = new XDocument();

                XElement userListXml = new XElement("users");
                userListXml.SetAttributeValue("count", users.Count());

                foreach (var user in users)
                {
                    XElement userXml = new XElement("user");

                    if (user.firstName != null)
                    {
                        userXml.SetAttributeValue("first-name", user.firstName);
                    }

                    userXml.SetAttributeValue("last-name", user.lastName);
                    userXml.SetAttributeValue("age", user.age);

                    XElement soldProductsXml = new XElement("sold-products");
                    soldProductsXml.SetAttributeValue("count", user.soldProducts.count);

                    foreach (var product in user.soldProducts.products)
                    {
                        XElement productXml = new XElement("product");
                        productXml.SetAttributeValue("name", product.productName);
                        productXml.SetAttributeValue("price", product.prductPrice);

                        soldProductsXml.Add(productXml);
                    }

                    userXml.Add(soldProductsXml);
                    userListXml.Add(userXml);
                }

                xDocument.Add(userListXml);
                xDocument.Save("XmlExports/users-and-products.xml");
            }
        }
    }
}
