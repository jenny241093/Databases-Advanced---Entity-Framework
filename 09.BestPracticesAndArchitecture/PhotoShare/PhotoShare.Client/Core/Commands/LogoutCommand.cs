using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    public class LogoutCommand
    {
        public static string Execute()
        {
            if (Session.User==null)
            {
                throw new ArgumentException("You should log in first in order to logout.");
            }
            var username = Session.User.Username;
            Session.User = null;

            return $"User {username} successfully logged out!";
        }
    }
}
