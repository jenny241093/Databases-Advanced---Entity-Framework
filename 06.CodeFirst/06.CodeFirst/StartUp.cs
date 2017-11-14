

namespace HospitalStartUp
{
    using System;
    using P01_HospitalDatabase.Data;
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data.Models;
    using P01_HospitalDatabase.Initializer;
    public class StartUp

    {
        static void Main(string[] args)
        {
            using (var db = new HospitalContext())
            {
                DatabaseInitializer.InitialSeed(db);
            }

        }
    }
}
