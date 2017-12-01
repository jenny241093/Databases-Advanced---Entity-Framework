using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class UploadPictureCommand
    {
        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public static string Execute(string[] data)
        {
            var albumName = data[0];
            var pictureTitle = data[1];
            var pictureFilePath = data[2];
            using (PhotoShareContext context=new PhotoShareContext())
            {
                if (Session.User == null)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }
                var album = context.Albums
                    .Include(p=>p.Pictures)
                    .SingleOrDefault(a => a.Name == albumName);
                if (album == null)
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }
                context.Pictures.Add(new Picture()
                {
                    Title = pictureTitle,
                    Album = album,
                    Path = pictureFilePath
                });
            }
            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}
