using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
   public class RegisterUserCommand
    {
      //•	RegisterUser<username> <password> <repeat-password> <firstName> <lastName> <age> <gender>
        public string Execute(string[]inputArgs)
        {
            Check.CheckLength(7,inputArgs);
            string userName = inputArgs[0];
            //validate given username

            if (userName.Length<Constants.MinUsernameLength||userName.Length>Constants.MaxUsernameLength)
            {
              throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid,userName));  
            }
            string password = inputArgs[1];

            //validate password
            if (!password.Any(char.IsDigit)||!password.Any(char.IsUpper))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid,password));
            }
            string repeatedPassword = inputArgs[2];

            //validate passwords
            if (password!=repeatedPassword)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }

            string firstName = inputArgs[3];
            string lastName = inputArgs[4];

            int age;
            bool isNumber = int.TryParse(inputArgs[5], out age);
            if (!isNumber||age<=0)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            Gender gender;
            bool isGenderValid = Enum.TryParse(inputArgs[6], out gender);
            if (!isGenderValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }

            if (CommandHelper.IsUserExisting(userName))
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken,userName));
            }

            this.RegisterUser(userName, password, firstName, lastName, age, gender);

            return $"User {userName} was registered successfully!";
        }

        private void RegisterUser(string username, string password, string firstname, string lastname, int age,
            Gender gender)
        {
            using (TeamBuilderContext context=new TeamBuilderContext())
            {
                User u=new User()
                {
                    Username = username,
                    Password = password,
                    FirstName = firstname,
                    LastName = lastname,
                    Age = age,
                    Gender = gender
                };

                context.Users.Add(u);
                context.SaveChanges();
            }
        }
    }
}
