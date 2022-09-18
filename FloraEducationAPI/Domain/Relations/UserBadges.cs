using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Domain.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.Relations
{
    public class UserBadges : BaseEntity
    { 
        public string Username { get; set; }
        public User User { get; set; }
        public Guid BadgeId { get; set; }
        public Badge Badge { get; set; }
    }
}
