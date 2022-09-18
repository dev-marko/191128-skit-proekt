using FloraEducationAPI.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.Models.Authentication
{
    public class User
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public virtual ICollection<UserBadges> Badges { get; set; }
        public ICollection<UserLikedPlants> LikedPlants { get; set; }
    }
}
