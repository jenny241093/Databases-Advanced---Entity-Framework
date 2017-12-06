using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
   public  class CreateTeamCommand
    {
        //<name> <acronym> <description>
        public string Execute(string[] inputArgs)
        {
           Check.CheckLength(2,inputArgs);
           
           var user= AuthenticationManager.GetCurrentUser();
          
            var name = inputArgs[0];
            var acronym = inputArgs[1];
            string description = null;
            if (CommandHelper.IsTeamExisting(name))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamExists,name));
            }
            if (acronym.Length!=3)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidAcronym,acronym);
            }
            var team = new Team()
            {
                Name = name,
                Description = description,
                Acronym = acronym,
                CreatorId = user.Id

            };
            using (var db=new TeamBuilderContext())
            {
               
                db.Teams.Add(team);
                db.SaveChanges();
                var teamId = db.Teams.SingleOrDefault(t => t.Name == name).Id;
                db.UserTeams.Add(new UserTeam()
                {
                    UserId = user.Id,
                    TeamId=teamId
                });
                db.SaveChanges();
            }
            return $"Team {name} successfully created!";
        }
    }
}
