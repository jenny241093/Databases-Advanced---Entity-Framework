using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.DataProcessor.Dtos;
using Instagraph.Models;
using Newtonsoft.Json.Linq;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private static string errorMessage = "Error: Invalid data.";
        private static string successMsg = "Successfully imported {0}.";
        public static string ImportPictures(InstagraphContext context, string jsonString)
        {

            var pictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            StringBuilder sb = new StringBuilder();

            var validatedPictures = new List<Picture>();

            foreach (var picture in pictures)
            {
                bool isValid = !string.IsNullOrWhiteSpace(picture.Path) && picture.Size > 0;

                bool pictureExist = context.Pictures.Any(p => p.Path == picture.Path) || validatedPictures.Any(p => p.Path == picture.Path);

                if (!isValid || pictureExist)
                {
                    sb.AppendLine(errorMessage);
                    continue;
                }

                validatedPictures.Add(picture);
                sb.AppendLine(String.Format(successMsg, $"Picture {picture.Path}"));
            }

            context.Pictures.AddRange(validatedPictures);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            
            UserDto[] usersFromJson = JsonConvert.DeserializeObject<UserDto[]>(jsonString);
            var result = new StringBuilder();
            var validUsers = new List<User>();
            foreach (var userDto in usersFromJson)
            {
                bool isValid = !string.IsNullOrWhiteSpace(userDto.Username)
                               && userDto.Username.Length <= 30 && !string.IsNullOrWhiteSpace(userDto.Password) &&
                               userDto.Password.Length <= 20 &&
                               !string.IsNullOrWhiteSpace(userDto.ProfilePicture);
                var picture = context.Pictures.FirstOrDefault(p => p.Path == userDto.ProfilePicture);
                //bool pictureExists = context.Pictures.Any(p => p.Path == userDto.ProfilePicture);
                bool userExists = validUsers.Any(u => u.Username == userDto.Username);
                
                if (!isValid||picture==null||userExists)
                {
                    result.AppendLine(errorMessage);
                    continue;                   
                }
                var user = Mapper.Map<User>(userDto);
                user.ProfilePicture = picture;
                validUsers.Add(user);
                result.AppendLine(string.Format(successMsg,$"User {user.Username}"));
            }
            context.Users.AddRange(validUsers);
            context.SaveChanges();
            return result.ToString().TrimEnd();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            UsersFollowersDto[] userFollowersFromJson = JsonConvert.DeserializeObject<UsersFollowersDto[]>(jsonString);
            var result = new StringBuilder();
            var validUserFollowers = new List<UserFollower>();
            foreach (var uf in userFollowersFromJson)
            {
                int? userId = context.Users.FirstOrDefault(u => u.Username == uf.User)?.Id;
                int? followeId = context.Users.FirstOrDefault(u => u.Username == uf.Follower)?.Id;
                bool alreadyFollowed = validUserFollowers.Any(u => u.UserId == userId && u.FollowerId == followeId);
                if (alreadyFollowed)
                {

                    result.AppendLine(errorMessage);
                    continue;
                }
                if (userId==null||followeId==null)
                {
                    result.AppendLine(errorMessage);
                    continue;
                }
              
               
                var userFollower=new UserFollower()
                {
                    UserId = userId.Value,
                    FollowerId = followeId.Value
                };
                validUserFollowers.Add(userFollower);

                result.AppendLine($@"Successfully imported Follower {uf.Follower} to User {uf.User}.");

            }
            context.UsersFollowers.AddRange(validUserFollowers);
            context.SaveChanges();
            return result.ToString().Trim();

        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);
            var xmlElements = xDoc.Root.Elements();
            var result=new StringBuilder();
            var posts=new List<Post>();
            foreach (var element in xmlElements)
            {
                string caption=element.Element("caption")?.Value;
                string username = element.Element("user")?.Value;
                string picture = element.Element("picture")?.Value;
                var isValidInput = !string.IsNullOrWhiteSpace(caption) && !string.IsNullOrWhiteSpace(username) &&
                                   !string.IsNullOrWhiteSpace(picture);
                if (!isValidInput)
                {
                    result.AppendLine(errorMessage);
                    continue;
                }
                int? userId = context.Users.FirstOrDefault(u => u.Username == username)?.Id;
                int? pictureId = context.Pictures.FirstOrDefault(p => p.Path == picture)?.Id;
                if (userId==null||pictureId==null)
                {
                    result.AppendLine(errorMessage);
                    continue;
                }
                var post=new Post()
                {
                    Caption = caption,
                    UserId = userId.Value,
                    PictureId = pictureId.Value
                };

                posts.Add(post);
                result.AppendLine(string.Format(successMsg, $"Post {caption}"));
            }
            context.Posts.AddRange(posts);
            context.SaveChanges();
           return result.ToString().TrimEnd();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);
            var xmlElements = xDoc.Root.Elements();
            var result = new StringBuilder();
            var comments = new List<Comment>();
            foreach (var comment in xmlElements)
            {

                string content = comment.Element("content")?.Value;
                string userName = comment.Element("user")?.Value;
                string postAsString = comment.Element("post")?.Attribute("id")?.Value;

                bool isValid = !string.IsNullOrWhiteSpace(content) && !string.IsNullOrWhiteSpace(userName)&&!string.IsNullOrWhiteSpace(postAsString);
                if (!isValid)
                {
                    result.AppendLine(errorMessage);
                    continue;
                }
                int postId = int.Parse(postAsString);
                var userId = context.Users.FirstOrDefault(u => u.Username == userName)?.Id;
                var postExists = context.Posts.Any(p => p.Id == postId);
                if (userId==null||!postExists)
                {
                    result.AppendLine(errorMessage);
                    continue;
                }
                var currentComment=new Comment()
                {
                    UserId = userId.Value,
                    PostId = postId,
                    Content = content
                };
                comments.Add(currentComment);
                result.AppendLine(string.Format(successMsg, $"Comment {content}"));
            }
            context.Comments.AddRange(comments);
            context.SaveChanges();
            return result.ToString().Trim();
        }
      
    }
}
