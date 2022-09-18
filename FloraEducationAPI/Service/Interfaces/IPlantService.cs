using FloraEducationAPI.Domain.Enumerations;
using FloraEducationAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Interfaces
{
    public interface IPlantService
    {
        Plant FetchPlantById(Guid id);
        Plant FetchPlantByName(string plantName);
        List<Plant> FetchAllPlants();
        List<Plant> FetchAllPlantsByType(PlantType plantType);
        Plant CreatePlant(Plant entity);
        Plant UpdatePlant(Plant entity);
        Plant DeletePlant(Plant entity);
        bool PlantExists(Guid id);
    }
}
