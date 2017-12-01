using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoShare.Data;

namespace PhotoShare.Client.Core.Commands
{
   public  class LoginCommand
    {
        public static string Execute(string[] data)
        {
            var userName = data[0];
            var password = data[1];

            using (PhotoShareContext context=new PhotoShareContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Username == userName&&u.Password==password);

                if (Session.User != null)
                {
                    throw new ArgumentException("You should logout first!");
                }
                if (user==null)
                {
                    throw new ArgumentException($"Invalid username or password!");
                }
                
                Session.User = user;
            }
            return $"User {userName} successfully logged in!";
        }
    }
}
