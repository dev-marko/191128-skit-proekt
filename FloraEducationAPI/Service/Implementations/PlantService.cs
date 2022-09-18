using FloraEducationAPI.Domain.Enumerations;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Implementations
{
    public class PlantService : IPlantService
    {
        private readonly IRepository<Plant> plantRepositoryGeneric;
        private readonly IPlantRepository plantRepository;

        public PlantService(IRepository<Plant> plantRepositoryGeneric, IPlantRepository plantRepository)
        {
            this.plantRepositoryGeneric = plantRepositoryGeneric;
            this.plantRepository = plantRepository;
        }

        public Plant CreatePlant(Plant entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(null, "Entity must not be null");
            }

            return plantRepositoryGeneric.Insert(entity);
        }

        public Plant DeletePlant(Plant entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(null, "Entity must not be null");
            }

            return plantRepositoryGeneric.Delete(entity);
        }

        public List<Plant> FetchAllPlants()
        {
            return plantRepositoryGeneric.FetchAll().ToList();
        }

        public List<Plant> FetchAllPlantsByType(PlantType plantType)
        {
            return plantRepositoryGeneric.FetchAll().Where(e => e.Type.Equals(plantType)).ToList();
        }

        public Plant FetchPlantById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid id value, Guid must not be empty");
            }

            return plantRepository.FetchPlantById(id);
        }

        public Plant FetchPlantByName(string plantName)
        {
            if(string.IsNullOrEmpty(plantName))
            {
                throw new ArgumentNullException(null, "Plant name must not be null or empty");
            }

            return plantRepositoryGeneric.FetchAll().SingleOrDefault(e => e.Name.Equals(plantName));
        }

        public bool PlantExists(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Guid must not be empty");
            }

            return FetchPlantById(id) != null;
        }

        public Plant UpdatePlant(Plant entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(null, "Entity must not be null");
            }

            return plantRepositoryGeneric.Update(entity);
        }
    }
}
