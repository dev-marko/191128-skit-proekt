using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Domain.Relations;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Implementations;
using FloraEducationAPI.Service.Interfaces;
using System.Text.Json;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models.Authentication;

namespace FloraEducationAPI.UnitTests
{
    [TestFixture]
    public class BadgeServiceTests
    {
        private Mock<IRepository<Badge>> mockBadgeRepository;
        private Mock<IRepository<UserBadges>> mockUserBadgesRepository;
        private Mock<IUserService> mockUserService;
        private IBadgeService badgeService;

        [OneTimeSetUp]
        public void Setup()
        {
            mockBadgeRepository = new Mock<IRepository<Badge>>(MockBehavior.Loose);
            mockUserBadgesRepository = new Mock<IRepository<UserBadges>>(MockBehavior.Loose);
            mockUserService = new Mock<IUserService>(MockBehavior.Loose);
            badgeService = new BadgeService(mockBadgeRepository.Object, mockUserBadgesRepository.Object, mockUserService.Object);
        }

        [Test]
        public void AddBadge_NameIsNotNullOrEmpty_ReturnBadgeEntity()
        {
            string name = "Rose";
            var badgeId = Guid.NewGuid();

            mockBadgeRepository.Setup(m => m.Insert(It.IsAny<Badge>()))
                .Returns(new Badge
                {
                    Id = badgeId,
                    Name = name,
                    Users = null
                });

            var actual = badgeService.AddBadge(name);
            var expected = new Badge
            {
                Id = badgeId,
                Name = name,
                Users = null
            };

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Users, actual.Users);
        }

        [Test]
        public void AddBadge_NameIsNull_ArgumentNullException()
        {
            string name = null;
            var ex = Assert.Throws<ArgumentNullException>(() => badgeService.AddBadge(name));
            Assert.That(ex.Message, Is.EqualTo("Badge name must not be null"));
        }

        [Test]
        public void AddBadge_NameIsEmpty_ArgumentNullException()
        {
            string name = string.Empty;
            var ex = Assert.Throws<ArgumentNullException>(() => badgeService.AddBadge(name));
            Assert.That(ex.Message, Is.EqualTo("Badge name must not be null"));
        }

        [Test]
        public void FetchBadgeByName_BadgeExists_ReturnBadgeEntity()
        {
            // Name is not null or empty
            string name = "Rose";
            var badge = new Badge
            {
                Id = Guid.NewGuid(),
                Name = name,
                Users = null
            };

            IEnumerable<Badge> badges = new List<Badge> {
                badge
            };

            mockBadgeRepository.Setup(m => m.FetchAll()).Returns(badges);

            var actual = badgeService.FetchBadgeByName(name);
            var expected = badge;

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Users, actual.Users);
        }

        [Test]
        public void FetchBadgeByName_BadgeDoesNotExists_ReturnNull()
        {
            // Name is not null or empty
            string name = "Rose";

            IEnumerable<Badge> badges = new List<Badge> {
                new Badge
                {
                    Id = Guid.Parse("04d63e6c-64c4-444e-ac22-ffefd085cd9e"),
                    Name = "Tulip",
                    Users = null
                }
            };

            mockBadgeRepository.Setup(m => m.FetchAll()).Returns(badges);

            var actual = badgeService.FetchBadgeByName(name);

            Assert.Null(actual);
        }

        [Test]
        public void FetchBadgeByName_NameIsNull_ReturnNull()
        {
            string name = null;

            IEnumerable<Badge> badges = new List<Badge> {
                new Badge
                {
                    Id = Guid.Parse("04d63e6c-64c4-444e-ac22-ffefd085cd9e"),
                    Name = "Tulip",
                    Users = null
                }
            };

            mockBadgeRepository.Setup(m => m.FetchAll()).Returns(badges);

            var actual = badgeService.FetchBadgeByName(name);

            Assert.Null(actual);
        }

        [Test]
        public void FetchBadgeByName_NameIsEmpty_ReturnNull()
        {
            string name = string.Empty;

            IEnumerable<Badge> badges = new List<Badge> {
                new Badge
                {
                    Id = Guid.Parse("04d63e6c-64c4-444e-ac22-ffefd085cd9e"),
                    Name = "Tulip",
                    Users = null
                }
            };

            mockBadgeRepository.Setup(m => m.FetchAll()).Returns(badges);

            var actual = badgeService.FetchBadgeByName(name);

            Assert.Null(actual);
        }

        [Test]
        public void AddBadgeToUser_UserBadgeDTOIsNull_ArgumentNullException()
        {
            UserBadgeDTO userBadgeDTO = null;
            var ex = Assert.Throws<ArgumentNullException>(() => badgeService.AddBadgeToUser(userBadgeDTO));
            Assert.That(ex.Message, Is.EqualTo("UserBadgeDTO must not be null"));
        }

        [Test]
        public void AddBadgeToUser_BadgeNameIsNull_ArgumentNullException()
        {
            UserBadgeDTO userBadgeDTO = new UserBadgeDTO
            {
                BadgeName = null,
                Username = "markos"
            };

            var ex = Assert.Throws<ArgumentNullException>(() => badgeService.AddBadgeToUser(userBadgeDTO));
            Assert.That(ex.Message, Is.EqualTo("Badge name must not be null or empty"));
        }

        [Test]
        public void AddBadgeToUser_BadgeNameIsEmpty_ArgumentNullException()
        {
            UserBadgeDTO userBadgeDTO = new UserBadgeDTO
            {
                BadgeName = string.Empty,
                Username = "markos"
            };

            var ex = Assert.Throws<ArgumentNullException>(() => badgeService.AddBadgeToUser(userBadgeDTO));
            Assert.That(ex.Message, Is.EqualTo("Badge name must not be null or empty"));
        }

        [Test]
        public void AddBadgeToUser_UsernameIsNull_ArgumentNullException()
        {
            UserBadgeDTO userBadgeDTO = new UserBadgeDTO
            {
                BadgeName = "Rose",
                Username = null
            };

            var ex = Assert.Throws<ArgumentNullException>(() => badgeService.AddBadgeToUser(userBadgeDTO));
            Assert.That(ex.Message, Is.EqualTo("Username must not be null or empty"));
        }

        [Test]
        public void AddBadgeToUser_UsernameIsEmpty_ArgumentNullException()
        {
            UserBadgeDTO userBadgeDTO = new UserBadgeDTO
            {
                BadgeName = "Rose",
                Username = string.Empty
            };

            var ex = Assert.Throws<ArgumentNullException>(() => badgeService.AddBadgeToUser(userBadgeDTO));
            Assert.That(ex.Message, Is.EqualTo("Username must not be null or empty"));
        }

        [Test]
        public void AddBadgeToUser_BadgeNotFound_ArgumentNullException()
        {
            UserBadgeDTO userBadgeDTO = new UserBadgeDTO
            {
                BadgeName = "Rose",
                Username = "markos"
            };

            IEnumerable<Badge> badges = new List<Badge> { };

            mockBadgeRepository.Setup(m => m.FetchAll()).Returns(badges);

            var ex = Assert.Throws<ArgumentNullException>(() => badgeService.AddBadgeToUser(userBadgeDTO));
            Assert.That(ex.Message, Is.EqualTo("Badge not found"));
        }

        [Test]
        public void AddBadgeToUser_UserNotFound_ArgumentNullException()
        {
            UserBadgeDTO userBadgeDTO = new UserBadgeDTO
            {
                BadgeName = "Rose",
                Username = "markos"
            };

            IEnumerable<Badge> badges = new List<Badge> {
                new Badge
                {
                    Id = Guid.Parse("04d63e6c-64c4-444e-ac22-ffefd085cd9e"),
                    Name = "Rose",
                    Users = null
                }
            };

            mockBadgeRepository.Setup(m => m.FetchAll()).Returns(badges);

            mockUserService.Setup(m => m.FetchUserByUsername(It.IsAny<string>())).Returns((User) null);

            var ex = Assert.Throws<ArgumentNullException>(() => badgeService.AddBadgeToUser(userBadgeDTO));
            Assert.That(ex.Message, Is.EqualTo("User not found"));
        }

        [Test]
        public void AddBadgeToUser_BadgeAndUserExist_ReturnUserBadgeEntity()
        {
            var badgeId = Guid.NewGuid();
            var badgeName = "Rose";
            var username = "markos";

            UserBadgeDTO userBadgeDTO = new UserBadgeDTO
            {
                BadgeName = badgeName,
                Username = username
            };

            var user = new User
            {
                Username = username
            };

            var badge = new Badge
            {
                Id = badgeId,
                Name = badgeName,
                Users = null
            };

            var userBadges = new UserBadges
            {
                Id = Guid.NewGuid(),
                Username = username,
                User = user,
                BadgeId = badgeId,
                Badge = badge
            };

            IEnumerable<Badge> badges = new List<Badge> {
                badge
            };

            mockUserService.Setup(m => m.FetchUserByUsername(username))
                .Returns(user);

            mockBadgeRepository.Setup(m => m.FetchAll()).Returns(badges);

            mockUserBadgesRepository.Setup(m => m.Insert(It.IsAny<UserBadges>()))
                .Returns(userBadges);


            var actual = badgeService.AddBadgeToUser(userBadgeDTO);
            var expected = userBadges;

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(expected.User, actual.User);
            Assert.AreEqual(expected.BadgeId, actual.BadgeId);
            Assert.AreEqual(expected.Badge, actual.Badge);
        }
    }
}