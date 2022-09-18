using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.Models
{
    public class Badge : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<UserBadges> Users { get; set; }
    }
}
