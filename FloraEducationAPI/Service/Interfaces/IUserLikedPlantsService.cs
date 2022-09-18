using FloraEducationAPI.Domain.Relations;
using FloraEducationAPI.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Interfaces
{
    public interface IUserLikedPlantsService
    {
        UserLikedPlants AddPlantToLikedPlants(UserLikedPlantDTO userLikedPlantsDTO);
    }
}
