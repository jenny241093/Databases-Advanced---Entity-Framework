using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AutoMapper.QueryableExtensions;
using Instagraph.Data;
using Instagraph.DataProcessor.Dtos;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var posts = context.Posts
                .Where(p => p.Comments.Count == 0)
                .OrderBy(p => p.Id)
                .ProjectTo<UsersWithoutCommentsDto>()
                .ToArray();
            string jsonString = JsonConvert.SerializeObject(posts, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

            return jsonString;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var posts = context.Users
                .Where(u => u.Posts.Any(p => p.Comments.Any(c => u.Followers.Any(f => f.FollowerId == c.UserId))))
                .OrderBy(u => u.Id)
                .ProjectTo<PopularUserDto>()
                .ToArray();
               
           
               
            
            string jsonString = JsonConvert.SerializeObject(posts, Formatting.Indented, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            return jsonString;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var users = context.Users.Select(u => new
            {
                Username=u.Username,
               PostsCommentsCount= u.Posts.Select(p=>p.Comments.Count).ToArray()
            }).ToArray();

            var xDoc = new XDocument(new XElement("users"));
            var userDtos = new List<UserTopPostDto>();
            foreach (var u in users)
            {
                int mostComments = 0;
                if (u.PostsCommentsCount.Any())
                {
                    mostComments = u.PostsCommentsCount.OrderByDescending(c => c).FirstOrDefault();
                }
               var userDto=new UserTopPostDto()
               {
                   Username = u.Username,
                   MostComments = mostComments
               };
                userDtos.Add(userDto);

               
            }
            userDtos= userDtos.OrderByDescending(e => e.MostComments).ThenBy(e => e.Username).ToList();
            foreach (var u in userDtos)
            {
                xDoc.Root.Add(new XElement("user",
                    new XElement("Username", u.Username),
                    new XElement("MostComments", u.MostComments)));
            }
            string result = xDoc.ToString();

             
            return result;
        }
    }
}
