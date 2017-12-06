using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace TeamBuilder.Models
{
    

    public class User
    {
        public User()
        {
            this.CreatedEvents = new List<Event>();
            this.UserTeams = new List<UserTeam>();
            this.CreatedUserTeams = new List<Team>();
            this.ReceivedInvitations = new List<Invitation>();
        }

        public int Id { get; set; }
        [MinLength(3)]
        public string  Username { get; set; }
        
        [StringLength(30, MinimumLength = 6)]
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9]).*")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Event> CreatedEvents { get; set; }
        public ICollection<UserTeam> UserTeams { get; set; }
        public ICollection<Team> CreatedUserTeams { get; set; }
        public ICollection<Invitation> ReceivedInvitations { get; set; }

       
    }
}
