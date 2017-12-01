using System;
using System.Linq;

namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;
    using Utilities;

    public class AddTagCommand
    {
        // AddTag <tag>
        public static string Execute(string[] data)
        {
            string tag = data[0].ValidateOrTransform();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (Session.User == null)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }
                if (context.Tags.Any(e=>e.Name.Contains(tag)))
                {
                    throw new ArgumentException($"Tag {tag} exists!");
                }
                context.Tags.Add(new Tag
                {
                    Name = tag
                });

                context.SaveChanges();
            }

            return "Tag"+tag + " was added successfully to database!";
        }
    }
}
