using System.Linq;
using PhotoShare.Data;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class ModifyUserCommand
    {
        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public static string Execute(string[] data)
        {
            string username = data[0];
            string property = data[1];
            string newValue = data[2];

            using (var db = new PhotoShareContext())
            {
                var user = db.Users.FirstOrDefault(e => e.Username == username);
                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }
                if (Session.User == null || Session.User.Username != user.Username)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }
                switch (property)
                {
                    case "Password":
                        if (!newValue.Any(c=>char.IsLower(c))||!newValue.Any(c=>char.IsDigit(c)))
                        {
                            throw  new ArgumentException($"Value {newValue} not valid."+Environment.NewLine+"Invalid Password!");
                        }
                        user.Password = newValue;
                        break;
                    case "BornTown":

                        var bornTown = db.Towns.FirstOrDefault(t => t.Name == newValue);
                        if (bornTown==null)
                        {
                            throw new ArgumentException($"Value {newValue} not valid." + Environment.NewLine + "Invalid Town!");
                        }
                        user.BornTown = bornTown;
                        break;
                       
                    case "CurrentTown":
                        var currentTown = db.Towns.FirstOrDefault(t => t.Name == newValue);
                        if (currentTown==null)
                        {
                            throw new ArgumentException($"Value {newValue} not valid." + Environment.NewLine + $"Town {newValue} not found!");
                        }
                        user.CurrentTown = currentTown;
                        break;
                        default:
                        throw new ArgumentException($"Property {property} not supported!");
                            break;
                }
                
                db.SaveChanges();
            }
            return $"User {username} {property} is {newValue}.";
        }
    }
}
