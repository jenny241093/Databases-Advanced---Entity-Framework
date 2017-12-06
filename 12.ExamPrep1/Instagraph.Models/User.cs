﻿using System.Collections.Generic;

namespace Instagraph.Models
{
    public class User
    {
        public int Id { get; set; }
        //max-30-unique
        public string Username { get; set; }
        //max-20-unique
        public string Password { get; set; }

        public int ProfilePictureId { get; set; }
        public Picture ProfilePicture { get; set; }

        public ICollection<UserFollower> Followers { get; set; }=new HashSet<UserFollower>();
        public ICollection<UserFollower> UsersFollowing { get; set; }=new HashSet<UserFollower>();
        public ICollection<Post> Posts { get; set; }=new HashSet<Post>();
        public ICollection<Comment> Comments { get; set; }=new HashSet<Comment>();


    }
}