using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhotoShare.Data;

namespace PhotoShare.Client.Core.Commands
{
  public  class ListFriendsCommand
    {
        public static string Execute(string[] data)
        {
            var userName = data[0];
            using (var context=new PhotoShareContext())
            {
                var user=context.Users.SingleOrDefault(u=>u.Username==userName);
                if (user==null)
                {
                    throw new ArgumentException($"User {userName} not found!");
                }

                var userFriends = context.Friendships
                    .Where(f=>f.UserId==user.Id)
                    .Select(f=>f.Friend.Username).ToList();

                if (!userFriends.Any())
                {
                    return "No friends for this user. :(";
                }
                var sb=new StringBuilder();
                sb.AppendLine("Friends:");
                userFriends.ForEach(fr=>sb.AppendLine($"-{fr}"));
                return sb.ToString().Trim();
            }
           
        }
    }
}
