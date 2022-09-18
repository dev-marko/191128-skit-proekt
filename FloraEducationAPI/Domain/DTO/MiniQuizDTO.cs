using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.DTO
{
    public class MiniQuizDTO
    {
        public Guid PlantId { get; set; }
        public string Title { get; set; }
    }
}
