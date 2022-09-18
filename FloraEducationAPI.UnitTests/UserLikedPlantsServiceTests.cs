using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Domain.Relations;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Implementations;
using FloraEducationAPI.Service.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FloraEducationAPI.UnitTests
{
    [TestFixture]
    public class UserLikedPlantsServiceTests
    {
        private Mock<IRepository<UserLikedPlants>> mockUserLikedPlantsRepository;
        private Mock<IPlantService> mockPlantService;
        private Mock<IUserService> mockUserService;
        private IUserLikedPlantsService userLikedPlantsService;

        [OneTimeSetUp]
        public void Setup()
        {
            mockUserLikedPlantsRepository = new Mock<IRepository<UserLikedPlants>>(MockBehavior.Loose);
            mockPlantService = new Mock<IPlantService>(MockBehavior.Loose);
            mockUserService = new Mock<IUserService>(MockBehavior.Loose);
            userLikedPlantsService = new UserLikedPlantsService(mockUserService.Object, mockPlantService.Object, mockUserLikedPlantsRepository.Object);
        }

        [Test]
        public void AddPlantToLikedPlants_UserLikedPlantDTOIsNull_ReturnArgumentNullException()
        {
            UserLikedPlantDTO userLikedPlantDTO = null;
            var ex = Assert.Throws<ArgumentNullException>(() => userLikedPlantsService.AddPlantToLikedPlants(userLikedPlantDTO));
            Assert.That(ex.Message, Is.EqualTo("UserLikedPlantDTO must not be null"));
        }

        [Test]
        public void AddPlantToLikedPlants_UsernameIsNull_ReturnArgumentNullException()
        {
            UserLikedPlantDTO userLikedPlantDTO = new UserLikedPlantDTO
            {
                Username = null,
                PlantId = Guid.Empty
            };
            var ex = Assert.Throws<ArgumentNullException>(() => userLikedPlantsService.AddPlantToLikedPlants(userLikedPlantDTO));
            Assert.That(ex.Message, Is.EqualTo("UserLikedPlantDTO properties must not be null or empty"));
        }

        [Test]
        public void AddPlantToLikedPlants_UsernameIsEmpty_ReturnArgumentNullException()
        {
            UserLikedPlantDTO userLikedPlantDTO = new UserLikedPlantDTO
            {
                Username = string.Empty,
                PlantId = Guid.Empty
            };
            var ex = Assert.Throws<ArgumentNullException>(() => userLikedPlantsService.AddPlantToLikedPlants(userLikedPlantDTO));
            Assert.That(ex.Message, Is.EqualTo("UserLikedPlantDTO properties must not be null or empty"));
        }

        [Test]
        public void AddPlantToLikedPlants_PlantIdIsEmpty_ReturnArgumentNullException()
        {
            UserLikedPlantDTO userLikedPlantDTO = new UserLikedPlantDTO
            {
                Username = "markos",
                PlantId = Guid.Empty
            };
            var ex = Assert.Throws<ArgumentNullException>(() => userLikedPlantsService.AddPlantToLikedPlants(userLikedPlantDTO));
            Assert.That(ex.Message, Is.EqualTo("UserLikedPlantDTO properties must not be null or empty"));
        }

        [Test]
        public void AddPlantToLikedPlants_UserNotFound_ReturnArgumentNullException()
        {
            UserLikedPlantDTO userLikedPlantDTO = new UserLikedPlantDTO
            {
                Username = "markos",
                PlantId = Guid.NewGuid()
            };

            mockUserService.Setup(m => m.FetchUserByUsername(It.IsAny<string>()))
                .Returns((User) null);

            var ex = Assert.Throws<ArgumentNullException>(() => userLikedPlantsService.AddPlantToLikedPlants(userLikedPlantDTO));
            Assert.That(ex.Message, Is.EqualTo("User not found"));
        }

        [Test]
        public void AddPlantToLikedPlants_PlantNotFound_ReturnArgumentNullException()
        {
            var plantId = Guid.NewGuid();
            UserLikedPlantDTO userLikedPlantDTO = new UserLikedPlantDTO
            {
                Username = "markos",
                PlantId = plantId
            };

            mockUserService.Setup(m => m.FetchUserByUsername(It.IsAny<string>()))
                .Returns(new User
                {
                    Username = "markos"
                });

            mockPlantService.Setup(m => m.FetchPlantById(plantId))
                .Returns((Plant) null);

            var ex = Assert.Throws<ArgumentNullException>(() => userLikedPlantsService.AddPlantToLikedPlants(userLikedPlantDTO));
            Assert.That(ex.Message, Is.EqualTo("Plant not found"));
        }

        [Test]
        public void AddPlantToLikedPlants_PlantAndUserExist_ReturnCommentEntity()
        {
            var username = "markos";
            var plantId = Guid.NewGuid();

            UserLikedPlantDTO userLikedPlantDTO = new UserLikedPlantDTO
            {
                Username = username,
                PlantId = plantId
            };

            var plant = new Plant
            {
                Id = plantId
            };

            var user = new User
            {
                Username = username
            };

            var userLikedPlants = new UserLikedPlants
            {
                Id = Guid.NewGuid(),
                User = user,
                Username = username,
                PlantId = plantId,
                Plant = plant
            };

            mockPlantService.Setup(m => m.FetchPlantById(plantId))
                .Returns(plant);

            mockUserService.Setup(m => m.FetchUserByUsername(username))
                .Returns(user);

            mockUserLikedPlantsRepository.Setup(m => m.Insert(It.IsAny<UserLikedPlants>()))
                .Returns(userLikedPlants);

            var actual = userLikedPlantsService.AddPlantToLikedPlants(userLikedPlantDTO);
            var expected = userLikedPlants;

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(expected.User, actual.User);
            Assert.AreEqual(expected.PlantId, actual.PlantId);
            Assert.AreEqual(expected.Plant, actual.Plant);
        }
    }
}
