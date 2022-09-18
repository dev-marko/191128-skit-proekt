using FloraEducationAPI.Domain.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.Models
{
    public class Comment : BaseEntity
    {
        public Plant Plant { get; set; }
        public User Author { get; set; }
        public string Content { get; set; }
    }
}
