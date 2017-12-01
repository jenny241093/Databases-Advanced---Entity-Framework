using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class AddFriendCommand
    {
        // AddFriend <username1> <username2>
        public static string Execute(string[] data)
        {
            var firstUserName = data[0];
            var secondUserName = data[1];
            using (var context=new PhotoShareContext())
            {
                if (Session.User == null || Session.User.Username != firstUserName)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }
                var firstUser = context.Users
                    .Include(u=>u.FriendsAdded)
                    .ThenInclude(fa=>fa.Friend)
                    .SingleOrDefault(u => u.Username == firstUserName);
                if (firstUser==null)
                {
                    throw new ArgumentException($"{firstUserName} not found!");
                }
                var addedFriend = context.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .SingleOrDefault(u => u.Username == secondUserName);
                if (addedFriend==null)
                {
                    throw new ArgumentException($"{secondUserName} not found!");
                }
                bool isAlreadyAdded = firstUser.FriendsAdded.Any(f => f.Friend == addedFriend);
                bool accepted = addedFriend.FriendsAdded.Any(f => f.Friend == firstUser);
                if (isAlreadyAdded&&!accepted)
                {
                    throw new InvalidOperationException("Friend request already sent");
                }
                if (isAlreadyAdded&& accepted)
                {
                    throw new InvalidOperationException($"{secondUserName} is already a friend to {firstUserName}");
                }

                firstUser.FriendsAdded.Add(new Friendship()
                {
                    User=firstUser,
                    Friend = addedFriend
                });
                context.SaveChanges();
                return $"Friend {secondUserName} added to {firstUserName}";
            }
        }
    }
}
