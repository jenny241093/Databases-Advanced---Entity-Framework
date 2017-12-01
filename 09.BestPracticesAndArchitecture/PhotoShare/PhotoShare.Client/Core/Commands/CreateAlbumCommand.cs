using System.Linq;
using PhotoShare.Client.Utilities;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class CreateAlbumCommand
    {
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public static string Execute(string []data)
        {
            var username = data[0];
            var albumTitle = data[1];
            var color = data[2];
            var tagsToChek = data.Skip(3).Select(tag=>TagUtilities.ValidateOrTransform(tag)).ToArray();

            using (var context=new PhotoShareContext())
            {
                if (Session.User == null || Session.User.Username != username)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }
                var user = context.Users.SingleOrDefault(e => e.Username == username);
                if (user==null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }
                if (context.Albums.Any(e=>e.Name==albumTitle))
                {
                    throw new ArgumentException($"Album {albumTitle} exists!!");
                }
                Color albumColor;
                var doesColorExist = Enum.TryParse(color, true, out albumColor);
                if (!doesColorExist)
                {
                    throw new ArgumentException($"Color {color} not found!");
                }
                var tags = context.Tags.ToList();
                foreach (var tag in tagsToChek)
                {
                    var doesTagExist = tags.Any(e => e.Name.Contains(tag.ValidateOrTransform()));
                    if (!doesTagExist)
                    {
                        throw new ArgumentException($"Invalid tags!");
                    }
                }
                Album album = new Album
                {
                    Name = albumTitle,
                    BackgroundColor = albumColor
                };
                context.AlbumRoles.Add(new AlbumRole
                {
                    User = user,
                    Album = album,
                    Role = Role.Owner
                });
                foreach (var tag in tagsToChek)
                {
                    var currentTag = context.Tags.SingleOrDefault(t => t.Name == tag);
                    context.AlbumTags.Add(new AlbumTag
                    {
                        Tag = currentTag,
                        Album = album
                    });
                }
                context.SaveChanges();
                return $"Album {albumTitle} successfully created!";

            }
        }
    }
}
