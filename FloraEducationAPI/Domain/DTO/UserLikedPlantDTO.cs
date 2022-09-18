using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.DTO
{
    public class UserLikedPlantDTO
    {
        public string Username { get; set; }
        public Guid PlantId { get; set; }
    }
}
