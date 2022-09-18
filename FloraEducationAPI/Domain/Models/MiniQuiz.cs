using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.Models
{
    public class MiniQuiz : BaseEntity
    {
        public Guid PlantId { get; set; }
        public Plant Plant { get; set; }
        public string Title { get; set; }
        public virtual ICollection<QuizQuestion> Questions { get; set; }
    }
}
