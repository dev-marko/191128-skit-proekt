using FloraEducationAPI.Domain.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.Models
{
    public class Plant : BaseEntity
    {
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public PlantType Type { get; set; }
        public string Description { get; set; }
        public string Predispositions { get; set; }
        public string Planting { get; set; }
        public string Maintenance { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
