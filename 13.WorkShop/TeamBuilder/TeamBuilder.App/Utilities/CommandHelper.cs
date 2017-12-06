using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Utilities
{
    class CommandHelper
    {
        public static bool IsTeamExisting(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Any(t => t.Name == teamName);
            }
        }

        public static bool IsUserExisting(string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Users.Any(t => t.Username == username&&t.IsDeleted==false);
            }
        }

        public static bool IsInviteExisting(string teamName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Invitations.Any(i=>i.Team.Name==teamName&&i.InvitedUserId==user.Id&&i.IsActive);
            }
        }

        public static bool IsUserCreatorOfTeam(string teamName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Any(t => t.Name == teamName && t.CreatorId == user.Id);
            }
        }

        public static bool IsUserCreatorOfEvent(string eventName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Events.Any(e => e.Name == eventName && e.CreatorId == user.Id);
            }
        }

        public static bool IsMemberOfTeam(string teamName, string username)
        {
            using(TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Single(t => t.Name == teamName)
                    .Members.Any(ut => ut.User.Username == username);
            }
        }

        public static bool IsEventExisting(string eventName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Events.Any(e => e.Name == eventName);
            }
        }

    }
}
