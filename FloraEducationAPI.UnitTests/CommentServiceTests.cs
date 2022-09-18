using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Domain.Models.Authentication;
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
    public class CommentServiceTests
    {
        private Mock<IRepository<Comment>> mockCommentRepository;
        private Mock<IPlantService> mockPlantService;
        private Mock<IUserService> mockUserService;
        private ICommentService commentService;

        [OneTimeSetUp]
        public void Setup()
        {
            mockCommentRepository = new Mock<IRepository<Comment>>(MockBehavior.Loose);
            mockPlantService = new Mock<IPlantService>(MockBehavior.Loose);
            mockUserService = new Mock<IUserService>(MockBehavior.Loose);
            commentService = new CommentService(mockCommentRepository.Object, mockPlantService.Object, mockUserService.Object);
        }

        [Test]
        public void AddCommentToPlant_CommentDTOIsNull_ReturnArgumentNullException()
        {
            CommentDTO commentDTO = null;
            var ex = Assert.Throws<ArgumentNullException>(() => commentService.AddCommentToPlant(commentDTO));
            Assert.That(ex.Message, Is.EqualTo("CommentDTO object must not be null"));
        }

        [Test]
        public void AddCommentToPlant_ContentIsNull_ReturnArgumentNullException()
        {
            CommentDTO commentDTO = new CommentDTO
            {
                Username = "markos",
                PlantId = Guid.Empty,
                Content = null
            };

            var ex = Assert.Throws<ArgumentNullException>(() => commentService.AddCommentToPlant(commentDTO));
            Assert.That(ex.Message, Is.EqualTo("Content must not be null or empty"));
        }

        [Test]
        public void AddCommentToPlant_ContentIsEmpty_ReturnArgumentNullException()
        {
            CommentDTO commentDTO = new CommentDTO
            {
                Username = "markos",
                PlantId = Guid.Empty,
                Content = string.Empty
            };

            var ex = Assert.Throws<ArgumentNullException>(() => commentService.AddCommentToPlant(commentDTO));
            Assert.That(ex.Message, Is.EqualTo("Content must not be null or empty"));
        }

        [Test]
        public void AddCommentToPlant_UsernameIsNull_ReturnArgumentNullException()
        {
            CommentDTO commentDTO = new CommentDTO
            {
                Username = null,
                PlantId = Guid.NewGuid(),
                Content = "content"
            };

            var ex = Assert.Throws<ArgumentNullException>(() => commentService.AddCommentToPlant(commentDTO));
            Assert.That(ex.Message, Is.EqualTo("Username must not be null or empty"));
        }

        [Test]
        public void AddCommentToPlant_UsernameIsEmpty_ReturnArgumentNullException()
        {
            CommentDTO commentDTO = new CommentDTO
            {
                Username = string.Empty,
                PlantId = Guid.NewGuid(),
                Content = "content"
            };

            var ex = Assert.Throws<ArgumentNullException>(() => commentService.AddCommentToPlant(commentDTO));
            Assert.That(ex.Message, Is.EqualTo("Username must not be null or empty"));
        }

        [Test]
        public void AddCommentToPlant_PlantIdIsEmpty_ReturnArgumentException()
        {
            CommentDTO commentDTO = new CommentDTO
            {
                Username = "markos",
                PlantId = Guid.Empty,
                Content = "content"
            };

            var ex = Assert.Throws<ArgumentException>(() => commentService.AddCommentToPlant(commentDTO));
            Assert.That(ex.Message, Is.EqualTo("Invalid id, guid must not be empty"));
         }

        [Test]
        public void AddCommentToPlant_PlantNotFound_ReturnArgumentNullException()
        {
            CommentDTO commentDTO = new CommentDTO
            {
                Username = "markos",
                PlantId = Guid.NewGuid(),
                Content = "content"
            };

            mockPlantService.Setup(m => m.FetchPlantById(It.IsAny<Guid>())).Returns((Plant) null);

            var ex = Assert.Throws<ArgumentNullException>(() => commentService.AddCommentToPlant(commentDTO));
            Assert.That(ex.Message, Is.EqualTo("Plant not found"));
        }

        [Test]
        public void AddCommentToPlant_UserNotFound_ReturnArgumentNullException()
        {
            CommentDTO commentDTO = new CommentDTO
            {
                Username = "user",
                PlantId = Guid.NewGuid(),
                Content = "content"
            };

            mockPlantService.Setup(m => m.FetchPlantById(It.IsAny<Guid>()))
                .Returns(new Plant
                {
                    Id = Guid.NewGuid()
                });

            var ex = Assert.Throws<ArgumentNullException>(() => commentService.AddCommentToPlant(commentDTO));
            Assert.That(ex.Message, Is.EqualTo("User not found"));
        }

        [Test]
        public void AddCommentToPlant_PlantAndUserExist_ReturnCommentEntity()
        {
            var username = "markos";
            var plantId = Guid.NewGuid();
            var content = "content";

            CommentDTO commentDTO = new CommentDTO
            {
                Username = username,
                PlantId = plantId,
                Content = content
            };

            var plant = new Plant
            {
                Id = plantId
            };

            var user = new User
            {
                Username = username
            };

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Plant = plant,
                Author = user,
                Content = content
            };

            mockPlantService.Setup(m => m.FetchPlantById(plantId))
                .Returns(plant);

            mockUserService.Setup(m => m.FetchUserByUsername(username))
                .Returns(user);

            mockCommentRepository.Setup(m => m.Insert(It.IsAny<Comment>()))
                .Returns(comment);

            var actual = commentService.AddCommentToPlant(commentDTO);
            var expected = comment;

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Plant.Id, actual.Plant.Id);
            Assert.AreEqual(expected.Author.Username, actual.Author.Username);
            Assert.AreEqual(expected.Content, actual.Content);
        }
    }
}
