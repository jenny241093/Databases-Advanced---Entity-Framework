using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class LoginCommand
    {
        //•	Login <username> <password>
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(2,inputArgs);

            var userName = inputArgs[0];
            var password = inputArgs[1];

            if (AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }
            User user = this.GetUserByCredentials(userName, password);
            if (user==null||!CommandHelper.IsUserExisting(userName))
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }
            AuthenticationManager.Login(user);
            return $"User {user.Username} successfully logged in!";
        }

        private User GetUserByCredentials(string username, string password)
        {
       
            using (TeamBuilderContext context=new TeamBuilderContext())
            {
                var user = context.Users
                    .FirstOrDefault(u => u.Username == username && u.Password == password&&!u.IsDeleted);

                return user;
            }
            
        }
    }
}
