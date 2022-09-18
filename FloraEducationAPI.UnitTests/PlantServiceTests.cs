using FloraEducationAPI.Domain.Enumerations;
using FloraEducationAPI.Domain.Models;
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
    public class PlantServiceTests
    {
        private Mock<IRepository<Plant>> mockPlantRepositoryGeneric;
        private Mock<IPlantRepository> mockPlantRepository;
        private IPlantService plantService;

        [OneTimeSetUp]
        public void Setup()
        {
            mockPlantRepositoryGeneric = new Mock<IRepository<Plant>>(MockBehavior.Loose);
            mockPlantRepository = new Mock<IPlantRepository>(MockBehavior.Loose);
            plantService = new PlantService(mockPlantRepositoryGeneric.Object, mockPlantRepository.Object);
        }

        [Test]
        public void FetchAllPlants_PlantsFound_ReturnPlantList()
        {
            IEnumerable<Plant> plants = new List<Plant> {
                new Plant
                {
                    Id = Guid.NewGuid()
                }
            };

            mockPlantRepositoryGeneric.Setup(m => m.FetchAll()).Returns(plants);
            var actual = plantService.FetchAllPlants();
            Assert.IsNotEmpty(actual);
        }

        [Test]
        public void FetchAllUsers_UsersNotFound_ReturnUserList()
        {
            IEnumerable<Plant> plants = new List<Plant> { };

            mockPlantRepositoryGeneric.Setup(m => m.FetchAll()).Returns(plants);
            var actual = plantService.FetchAllPlants();
            Assert.IsEmpty(actual);
        }

        [Test]
        public void FetchAllPlantsByType_PlantsFound_ReturnPlantList()
        {
            var id = Guid.NewGuid();
            var type = PlantType.Цвеќиња;
            IEnumerable<Plant> plants = new List<Plant> {
                new Plant
                {
                    Id = id,
                    Type = type
                }
            };

            mockPlantRepositoryGeneric.Setup(m => m.FetchAll()).Returns(plants);
            var actual = plantService.FetchAllPlantsByType(type);
            Assert.IsNotEmpty(actual);
        }

        [Test]
        public void FetchAllPlantsByType_PlantsNotFound_ReturnEmptyList()
        {
            var id = Guid.NewGuid();
            var type = PlantType.Цвеќиња;
            IEnumerable<Plant> plants = new List<Plant> {
                new Plant
                {
                    Id = id,
                    Type = PlantType.Зеленчук
                }
            };

            mockPlantRepositoryGeneric.Setup(m => m.FetchAll()).Returns(plants);
            var actual = plantService.FetchAllPlantsByType(type);
            Assert.IsEmpty(actual);
        }

        [Test]
        public void FetchPlantById_IdIsEmpty_ReturnArgumentException()
        {
            Guid id = Guid.Empty;
            var ex = Assert.Throws<ArgumentException>(() => plantService.FetchPlantById(id));
            Assert.That(ex.Message, Is.EqualTo("Invalid id value, Guid must not be empty"));
        }

        [Test]
        public void FetchPlantByName_NameIsNull_ArgumentNullException()
        {
            string name = null;
            var ex = Assert.Throws<ArgumentNullException>(() => plantService.FetchPlantByName(name));
            Assert.That(ex.Message, Is.EqualTo("Plant name must not be null or empty"));
        }

        [Test]
        public void FetchPlantByName_NameIsEmpty_ArgumentNullException()
        {
            string name = string.Empty;
            var ex = Assert.Throws<ArgumentNullException>(() => plantService.FetchPlantByName(name));
            Assert.That(ex.Message, Is.EqualTo("Plant name must not be null or empty"));
        }

        [Test]
        public void FetchPlantByName_PlantFound_ReturnPlantEntity()
        {
            var name = "Rose";
            var plant = new Plant
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            IEnumerable<Plant> plants = new List<Plant> {
                plant
            };

            mockPlantRepositoryGeneric.Setup(m => m.FetchAll()).Returns(plants);
            var actual = plantService.FetchPlantByName(name);
            var expected = plant;

            Assert.AreEqual(expected.Name, actual.Name);
        }

        [Test]
        public void FetchPlantByName_PlantNotFound_ReturnNull()
        {
            var name = "Rose";
            var plant = new Plant
            {
                Id = Guid.NewGuid(),
                Name = "Tulip"
            };

            IEnumerable<Plant> plants = new List<Plant> {
                plant
            };

            mockPlantRepositoryGeneric.Setup(m => m.FetchAll()).Returns(plants);
            var actual = plantService.FetchPlantByName(name);
            Assert.IsNull(actual);
        }

        [Test]
        public void PlantExists_IdIsEmpty_ReturnArgumentException()
        {
            Guid id = Guid.Empty;
            var ex = Assert.Throws<ArgumentException>(() => plantService.PlantExists(id));
            Assert.That(ex.Message, Is.EqualTo("Guid must not be empty"));
        }

        [Test]
        public void PlantExists_PlantFound_ReturnTrue()
        {
            Guid id = Guid.NewGuid();

            var plant = new Plant
            {
                Id = id
            };

            mockPlantRepository.Setup(m => m.FetchPlantById(id)).Returns(plant);

            var actual = plantService.PlantExists(id);
            Assert.True(actual);
        }

        [Test]
        public void PlantExists_PlantNotFound_ReturnFalse()
        {
            Guid id = Guid.NewGuid();

            mockPlantRepositoryGeneric.Setup(m => m.FetchById(id)).Returns((Plant) null);

            var actual = plantService.PlantExists(id);
            Assert.False(actual);
        }

        [Test]
        public void CreatePlant_PlantIsNull_ReturnArgumentNullException()
        {
            Plant plant = null;

            mockPlantRepositoryGeneric.Setup(m => m.Insert(plant)).Throws<ArgumentNullException>();

            var ex = Assert.Throws<ArgumentNullException>(() => plantService.CreatePlant(plant));
            Assert.That(ex.Message, Is.EqualTo("Entity must not be null"));
         }   

        [Test]
        public void CreatePlant_PlantIsNotNull_ReturnPlantEntity()
        {
            Plant plant = new Plant { };

            mockPlantRepositoryGeneric.Setup(m => m.Insert(plant)).Returns(plant);

            var actual = plantService.CreatePlant(plant);

            Assert.IsNotNull(actual);
        }

        [Test]
        public void UpdatePlant_PlantIsNull_ReturnArgumentNullException()
        {
            Plant plant = null;

            mockPlantRepositoryGeneric.Setup(m => m.Update(plant)).Throws<ArgumentNullException>();

            var ex = Assert.Throws<ArgumentNullException>(() => plantService.UpdatePlant(plant));
            Assert.That(ex.Message, Is.EqualTo("Entity must not be null"));
        }

        [Test]
        public void UpdatePlant_PlantIsNotNull_ReturnPlantEntity()
        {
            Plant plant = new Plant {};

            mockPlantRepositoryGeneric.Setup(m => m.Update(plant)).Returns(plant);

            var actual = plantService.UpdatePlant(plant);

            Assert.IsNotNull(actual);
        }

        [Test]
        public void DeletePlant_PlantIsNull_ReturnArgumentNullException()
        {
            Plant plant = null;

            mockPlantRepositoryGeneric.Setup(m => m.Delete(plant)).Throws<ArgumentNullException>();

            var ex = Assert.Throws<ArgumentNullException>(() => plantService.DeletePlant(plant));
            Assert.That(ex.Message, Is.EqualTo("Entity must not be null"));
        }

        [Test]
        public void DeletePlant_PlantIsNotNull_ReturnPlantEntity()
        {
            Plant plant = new Plant { };

            mockPlantRepositoryGeneric.Setup(m => m.Delete(plant)).Returns(plant);

            var actual = plantService.DeletePlant(plant);

            Assert.IsNotNull(actual);
        }
    }
}
