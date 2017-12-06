using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    class AcceptInviteCommand
    {
        //teamName>
        public string Execute(string[] inputArgs)
        {

            Check.CheckLength(1, inputArgs);
            AuthenticationManager.Authorize();
            string teamName = inputArgs[0];
            var currentUser = AuthenticationManager.GetCurrentUser();



            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            if (!CommandHelper.IsInviteExisting(teamName, currentUser))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound, teamName));
            }
            using (var db = new TeamBuilderContext())
            {

                var teamId = db.Teams.FirstOrDefault(t => t.Name == teamName).Id;

                var userTeam = new UserTeam()
                {
                    TeamId = teamId,
                    UserId = currentUser.Id
                };
                db.UserTeams.Add(userTeam);
                db.SaveChanges();


                var invitation = db.Invitations.FirstOrDefault(i =>
                    i.InvitedUserId == currentUser.Id && i.TeamId == teamId && i.IsActive);
                invitation.IsActive = false;
                db.SaveChanges();
                return $"User {currentUser.Username} joined team {teamName}!";
            }
        }
    }
}