using System;
using P03_SalesDatabase.Data;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase
{
    class StartUp
    {
        public static void Main()
        {

            var db = new SalesContext();
            //using (var db = new SalesContext())
            //{
            //    db.Database.EnsureDeleted();
            //}
        }
    }
}
