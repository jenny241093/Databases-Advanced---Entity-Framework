using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class AcceptFriendCommand
    {
        // AcceptFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            var firstUserName = data[0];
            var secondUserName = data[1];
            using (PhotoShareContext context=new PhotoShareContext())
            {
                var firstUser = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(af => af.Friend)
                    .SingleOrDefault(u => u.Username == firstUserName);

                var accepterUser=context.Users
                    .Include(u => u.AddedAsFriendBy)
                    .ThenInclude(af => af.Friend)
                    .SingleOrDefault(u => u.Username == secondUserName);

                if (accepterUser==null)
                {
                    throw new ArgumentException($"{secondUserName} not found!");
                }
                if (firstUser == null)
                {
                    throw new ArgumentException($"{firstUserName} not found!");
                }
                bool hasFirstUserSentRequest = firstUser.FriendsAdded.Any(u => u.Friend == accepterUser);
                bool hasAccepterAcceptedRequest = accepterUser.AddedAsFriendBy.Any(u => u.Friend == firstUser);
                if (hasAccepterAcceptedRequest&&hasFirstUserSentRequest)
                {
                    throw new InvalidOperationException($"{secondUserName} is already a friend to {firstUserName}");
                }
                if (!hasFirstUserSentRequest)
                {
                    throw new InvalidOperationException($"{firstUserName} has not added{secondUserName} as a friend");

                }

                var friendship = new Friendship()
                {
                    UserId = accepterUser.Id,
                    FriendId = firstUser.Id
                };
                accepterUser.AddedAsFriendBy.Add(friendship);
                context.SaveChanges();
            }
            return $"{secondUserName} accepted {firstUserName} as a friend";
        }
    }
}
