using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
    public class InviteToTeamCommand
    {
      //  teamName> <username>
        public string Execute(string[] inputArgs)
        {
            Check.CheckLength(2,inputArgs);

            var teamName = inputArgs[0];
            var userName = inputArgs[1];

            var currentUser = AuthenticationManager.GetCurrentUser();
            if (!CommandHelper.IsUserCreatorOfTeam(teamName,currentUser))
            {
                throw new ArgumentException(Constants.ErrorMessages.NotAllowed);
            }
            if (!CommandHelper.IsUserExisting(userName)||!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
            }
            if (CommandHelper.IsInviteExisting(teamName,currentUser))
            {
                throw new ArgumentException(Constants.ErrorMessages.InviteIsAlreadySent);
            }
            using (var db=new TeamBuilderContext())
            {
                //bool hasTheInvitationBeenSent=db.Invitations.FirstOrDefault(i=>i.IsActive)

                var userToInvite = db.Users.FirstOrDefault(u => u.Username == userName);
                var team = db.Teams.FirstOrDefault(t => t.Name == teamName);

                var invitation = new Invitation()
                {
                    InvitedUserId = userToInvite.Id,
                    TeamId = team.Id,
                   
                };
                db.Invitations.Add(invitation);
                db.SaveChanges();
            }
            return $"Team {teamName} invited {userName}!";
        }
    }
}
