using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using P01_ProductsShop.App.JsonQueries;
using P01_ProductsShop.App.XmlQueries;
using P01_ProductsShop.Models;

namespace P01_ProductsShop.App
{
    using P01_ProductsShop.Data;
    using Newtonsoft.Json;

    class StartUp
    {
        static void Main(string[] args)
        {
            // 1.Import data
            //using (var db = new ProductShopSystemContext())
            //{
            //    db.Database.EnsureDeleted();
            //    db.Database.EnsureCreated();
            //}
            ////1.---ImportUsers
            // JsonImport.ImportUsersFromJson();

            // ////2.---ImportCategories
            // JsonImport.ImportCategoriesFromJson();

            // ////3.---ImportProducts

            // JsonImport.ImportProductsFromJson();
            // //4.---SetCategoriesFromJson
            // JsonImport.SetCategoriesFromJson();

            // // 2. Query and Export Data

            // //Query 1 - Products In Range
            //ProductsInRange.GetproductsInRange();

            // //Query 2 - Successfully Sold Products
            //SuccessfullySoldProducts.GetSuccessfullySoldProducts();

            // // Query 3 - Categories By Products Count
            //CategoriesByProductsCount.GetCategoriesByProductsCount();

            // //Query 4 - Users and Products

            //UsersAndProducts.GetUsersWithAtLeastOneSoldProduct();

            //XML Processing
            //using (var db = new ProductShopSystemContext())
            //{
            //    db.Database.EnsureDeleted();
            //    db.Database.EnsureCreated();
            //}

            //1.Import Data
            //XmlImport.ImportUsersFromXml();
            //XmlImport.ImportCategoriesFromXml();
            //XmlImport.ImportProductsFromXml();
            //XmlImport.SetCategoriesFromXml();
            //2.Query and Export Data
            using (var db=new ProductShopSystemContext())
            {  //1---Products In Range
         //Xml.ProductsInRange(db);
               //2---Products In Range
         // Xml.SoldProducts(db);

                //3---Categories By Products Count
            //Xml.CategoriesByeProductsCount(db);

                //4---Users and Products
               // Xml.UsersAndProducts(db);
            }
        }
    }
}
