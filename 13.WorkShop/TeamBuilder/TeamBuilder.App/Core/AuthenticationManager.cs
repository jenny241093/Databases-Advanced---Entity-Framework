using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic;
using TeamBuilder.Models;
using Constants = TeamBuilder.App.Utilities.Constants;

namespace TeamBuilder.App.Core
{
  public class AuthenticationManager
  {
      private static User currentUser;
    //– saves given user as logged user until logout or exit of the application
     public static void Login(User user)
        {
            if (currentUser!=null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }
            currentUser = user;
        }
        //– logs out currently logged in user, if there is none should throw exception(use the method below)
     public static void Logout()
        {
            Authorize();
            currentUser = null;
        }
        //– throws InvalidOperationException if there is no logged in user
       public static void Authorize()
        {
            if (currentUser==null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }
        //– returns true if there is logged in user else returns false
       public static bool IsAuthenticated()
        {
            if (currentUser!=null)
            {
                return true;
            }
            return false;
        }
        //gets currently logged in user if there is not throws exception
      public  static User GetCurrentUser()
        {
            Authorize();
            return currentUser;
        }

    }
}
