using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.DTO
{
    public class CommentDTO
    {
        public Guid PlantId { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }
    }
}
