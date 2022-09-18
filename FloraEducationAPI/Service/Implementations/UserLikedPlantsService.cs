using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Domain.Relations;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Implementations
{
    public class UserLikedPlantsService : IUserLikedPlantsService
    {
        private readonly IUserService userService;
        private readonly IPlantService plantService;
        private readonly IRepository<UserLikedPlants> userLikedPlantsRepository;

        public UserLikedPlantsService(IUserService userService, IPlantService plantService, IRepository<UserLikedPlants> userLikedPlantsRepository)
        {
            this.userService = userService;
            this.plantService = plantService;
            this.userLikedPlantsRepository = userLikedPlantsRepository;
        }

        public UserLikedPlants AddPlantToLikedPlants(UserLikedPlantDTO userLikedPlantsDTO)
        {
            if (userLikedPlantsDTO == null)
            {
                throw new ArgumentNullException(null, "UserLikedPlantDTO must not be null");
            }

            if (string.IsNullOrEmpty(userLikedPlantsDTO.Username) || (userLikedPlantsDTO.PlantId == Guid.Empty))
            {
                throw new ArgumentNullException(null, "UserLikedPlantDTO properties must not be null or empty");
            }

            User user = userService.FetchUserByUsername(userLikedPlantsDTO.Username);

            if (user == null)
            {
                throw new ArgumentNullException(null, "User not found");
            }

            Plant plant = plantService.FetchPlantById(userLikedPlantsDTO.PlantId);

            if (plant == null)
            {
                throw new ArgumentNullException(null, "Plant not found");
            }

            UserLikedPlants userLikedPlant = new UserLikedPlants
            {
                Username = user.Username,
                User = user,
                PlantId = plant.Id,
                Plant = plant
            };

            return userLikedPlantsRepository.Insert(userLikedPlant);
        }
    }
}
