using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class ShareAlbumCommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public static string Execute(string [] data)
        {
            var albumId = int.Parse(data[0]);
            var userName = data[1];
            var permission = data[2];
         
            using (PhotoShareContext context=new PhotoShareContext())
            {
                if (Session.User == null || Session.User.Username != userName)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }
                var album = context.Albums
                    .Include(a=>a.AlbumRoles)
                    .SingleOrDefault
                    (a => a.Id == albumId);
                if (album==null)
                {
                    throw new ArgumentException($"Album {albumId} not found!");
                }
                var user = context.Users.SingleOrDefault(u => u.Username == userName);
                if (user==null)
                {
                    throw  new ArgumentException($"User {userName} not found!");
                }

                Role role;
                var doesRoleExist = Enum.TryParse(permission, true, out role);
                if (!doesRoleExist)
                {
                    throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
                }
                context.AlbumRoles.Add(new AlbumRole
                    {
                        User = user,
                        Album = album,
                        Role = role
                    }
                );
                context.SaveChanges();
                return $"Username {userName} added to album {album.Name} ({permission})";
            }
        }
    }
}
