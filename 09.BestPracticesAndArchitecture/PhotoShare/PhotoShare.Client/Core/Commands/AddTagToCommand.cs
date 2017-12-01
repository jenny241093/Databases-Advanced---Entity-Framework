using System.Linq;
using PhotoShare.Client.Utilities;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class AddTagToCommand 
    {
        // AddTagTo <albumName> <tag>
        public static string Execute(string [] data)
        {
            var albumName = data[0];
            var tagName = data[1];

            using (var context = new PhotoShareContext())
            {
                if (Session.User == null)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }
                var album = context.Albums.SingleOrDefault(a => a.Name == albumName);
                var tag = context.Tags.SingleOrDefault(t => t.Name == tagName.ValidateOrTransform());
                if (album==null|| tag==null)
                {
                    throw  new ArgumentException($"Either tag or album do not exist!");
                }
                context.AlbumTags.Add(new AlbumTag
                {
                    Album = album,
                    Tag = tag
                });
                context.SaveChanges();
                return $"Tag {tagName} added to {albumName}!";

            }
        }
    }
}
