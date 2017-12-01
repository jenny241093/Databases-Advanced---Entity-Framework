using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;

    public class AddTownCommand
    {
        // AddTown <townName> <countryName>
        public static string Execute(string[] data)
        {
            string townName = data[0];
            string country = data[1];


            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (Session.User == null)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }
                var isTheTownAdded = context.Towns.Any(t => t.Name.Contains(townName));
                if (isTheTownAdded)
                {
                    throw new ArgumentException($"Town {townName} was already added!");
                }
               
                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                context.Towns.Add(town);
                context.SaveChanges();

                return townName + " was added successfully!";
            }
        }
    }
}
