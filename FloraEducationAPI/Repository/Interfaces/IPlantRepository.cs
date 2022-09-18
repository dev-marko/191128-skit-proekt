using FloraEducationAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Repository.Interfaces
{
    public interface IPlantRepository
    {
        Plant FetchPlantById(Guid plantId);
    }
}
