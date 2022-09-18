using FloraEducationAPI.Domain.DTO.Authentication;
using FloraEducationAPI.Domain.Enumerations;
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
    public class UserServiceTests
    {
        private Mock<IUserRepository> mockUserRepository;
        private IUserService userService;

        [OneTimeSetUp]
        public void Setup()
        {
            mockUserRepository = new Mock<IUserRepository>(MockBehavior.Loose);
            userService = new UserService(mockUserRepository.Object);
        }

        [Test]
        public void FetchAllUsers_UsersFound_ReturnUserList()
        {
            IEnumerable<User> users = new List<User> {
                new User 
                {
                    Username = "markos"
                }
            };

            mockUserRepository.Setup(m => m.FetchAllUsers()).Returns(users);
            var actual = userService.FetchAllUsers();
            Assert.IsNotEmpty(actual);
        }

        [Test]
        public void FetchAllUsers_UsersNotFound_ReturnEmptyList()
        {
            IEnumerable<User> users = new List<User>();

            mockUserRepository.Setup(m => m.FetchAllUsers()).Returns(users);
            var actual = userService.FetchAllUsers();
            Assert.IsEmpty(actual);
        }

        [Test]
        public void FetchUserByEmail_UserFound_ReturnUserEntity()
        {
            // Email valid (not null or empty)
            string email = "marko@gmail.com";
            var user = new User
            {
                Email = email
            };

            mockUserRepository.Setup(m => m.FetchUserByEmail(email))
                .Returns(user);

            var actual = userService.FetchUserByEmail(email);
            var expected = user;

            Assert.AreEqual(expected.Email, actual.Email);
        }

        [Test]
        public void FetchUserByEmail_UserNotFound_ReturnNullUserEntity()
        {
            // Email valid (not null or empty)
            string email = "marko@gmail.com";

            mockUserRepository.Setup(m => m.FetchUserByEmail(email))
                .Returns((User) null);

            var actual = userService.FetchUserByEmail(email);

            Assert.IsNull(actual);
        }

        [Test]
        public void FetchUserByEmail_EmailIsNull_ReturnArgumentNullException()
        {
            string email = null;
            var user = new User
            {
                Email = email
            };

            mockUserRepository.Setup(m => m.FetchUserByEmail(email))
                .Returns(user);

            var ex = Assert.Throws<ArgumentNullException>(() => userService.FetchUserByEmail(email));
            Assert.That(ex.Message, Is.EqualTo("Email must not be null or empty"));
        }

        [Test]
        public void FetchUserByEmail_EmailIsEmpty_ReturnArgumentNullException()
        {
            string email = string.Empty;
            var user = new User
            {
                Email = email
            };

            mockUserRepository.Setup(m => m.FetchUserByEmail(email))
                .Returns(user);

            var ex = Assert.Throws<ArgumentNullException>(() => userService.FetchUserByEmail(email));
            Assert.That(ex.Message, Is.EqualTo("Email must not be null or empty"));
        }

        [Test]
        public void FetchUserByUsername_UserFound_ReturnUserEntity()
        {
            // Username valid (not null or empty)
            string username = "markos";
            var user = new User
            {
                Username = username
            };

            mockUserRepository.Setup(m => m.FetchUserByUsername(username))
                .Returns(user);

            var actual = userService.FetchUserByUsername(username);
            var expected = user;

            Assert.AreEqual(expected.Username, actual.Username);
        }

        [Test]
        public void FetchUserByUsername_UserNotFound_ReturnNullUserEntity()
        {
            // Username valid (not null or empty)
            string username = "markos";
            User user = null;

            mockUserRepository.Setup(m => m.FetchUserByUsername(username))
                .Returns(user);

            var actual = userService.FetchUserByUsername(username);

            Assert.IsNull(actual);
        }

        [Test]
        public void FetchUserByUsername_UsernameIsNull_ReturnArgumentNullException()
        {
            string username = null;
            var user = new User
            {
                Username = username
            };

            mockUserRepository.Setup(m => m.FetchUserByUsername(username))
                .Returns(user);

            var ex = Assert.Throws<ArgumentNullException>(() => userService.FetchUserByUsername(username));
            Assert.That(ex.Message, Is.EqualTo("Username must not be null or empty"));
        }

        [Test]
        public void FetchUserByUsername_UsernameIsEmpty_ReturnArgumentNullException()
        {
            string username = string.Empty;
            var user = new User
            {
                Username = username
            };

            mockUserRepository.Setup(m => m.FetchUserByUsername(username))
                .Returns(user);

            var ex = Assert.Throws<ArgumentNullException>(() => userService.FetchUserByUsername(username));
            Assert.That(ex.Message, Is.EqualTo("Username must not be null or empty"));
        }

        [Test]
        public void Authenticate_UserLoginDTOIsNull_ReturnArgumentNullException()
        {
            UserLoginDTO userLoginDTO = null;
            var ex = Assert.Throws<ArgumentNullException>(() => userService.Authenticate(userLoginDTO));
            Assert.That(ex.Message, Is.EqualTo("UserLoginDTO must not be null"));
        }

        [Test]
        public void Authenticate_UsernameIsNull_ReturnArgumentNullException()
        {
            UserLoginDTO userLoginDTO = new UserLoginDTO 
            {
                Username = null,
                Password = "pass"
            };

            var ex = Assert.Throws<ArgumentNullException>(() => userService.Authenticate(userLoginDTO));
            Assert.That(ex.Message, Is.EqualTo("Username must not be null or empty"));
        }

        [Test]
        public void Authenticate_UsernameIsEmpty_ReturnArgumentNullException()
        {
            UserLoginDTO userLoginDTO = new UserLoginDTO
            {
                Username = string.Empty,
                Password = "pass"
            };

            var ex = Assert.Throws<ArgumentNullException>(() => userService.Authenticate(userLoginDTO));
            Assert.That(ex.Message, Is.EqualTo("Username must not be null or empty"));
        }

        [Test]
        public void Authenticate_PasswordIsNull_ReturnArgumentNullException()
        {
            UserLoginDTO userLoginDTO = new UserLoginDTO
            {
                Username = "markos",
                Password = null
            };

            var ex = Assert.Throws<ArgumentNullException>(() => userService.Authenticate(userLoginDTO));
            Assert.That(ex.Message, Is.EqualTo("Password must not be null or empty"));
        }

        [Test]
        public void Authenticate_PasswordIsEmpty_ReturnArgumentNullException()
        {
            UserLoginDTO userLoginDTO = new UserLoginDTO
            {
                Username = "markos",
                Password = string.Empty
            };

            var ex = Assert.Throws<ArgumentNullException>(() => userService.Authenticate(userLoginDTO));
            Assert.That(ex.Message, Is.EqualTo("Password must not be null or empty"));
        }

        [Test]
        public void Register_UserRegisterDTOIsNull_ReturnArgumentNullException()
        {
            UserRegisterDTO userRegisterDTO = null;
            var ex = Assert.Throws<ArgumentNullException>(() => userService.Register(userRegisterDTO));
            Assert.That(ex.Message, Is.EqualTo("UserRegisterDTO must not be null"));
        }

        [Test]
        public void Register_UserRegisterDTOAnyPropsAreNull_ReturnArgumentNullException()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO
            {
                Email = null,
                Username = "markos",
                Password = null,
                Name = null,
                Surname = null
            };

            var ex = Assert.Throws<ArgumentNullException>(() => userService.Register(userRegisterDTO));
            Assert.That(ex.Message, Is.EqualTo("All fields must not be null or empty"));
        }

        [Test]
        public void Register_UserRegisterDTOAnyPropsAreEmpty_ReturnArgumentNullException()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO
            {
                Email = string.Empty,
                Username = "markos",
                Password = string.Empty,
                Name = string.Empty,
                Surname = string.Empty
            };

            var ex = Assert.Throws<ArgumentNullException>(() => userService.Register(userRegisterDTO));
            Assert.That(ex.Message, Is.EqualTo("All fields must not be null or empty"));
        }

        [Test]
        public void Register_UserRegisterDTOAllPropsAreNull_ReturnArgumentNullException()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO
            {
                Email = null,
                Username = null,
                Password = null,
                Name = null,
                Surname = null
            };

            var ex = Assert.Throws<ArgumentNullException>(() => userService.Register(userRegisterDTO));
            Assert.That(ex.Message, Is.EqualTo("All fields must not be null or empty"));
        }

        [Test]
        public void Register_UserRegisterDTOAllPropsAreEmpty_ReturnArgumentNullException()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO
            {
                Email = string.Empty,
                Username = string.Empty,
                Password = string.Empty,
                Name = string.Empty,
                Surname = string.Empty
            };

            var ex = Assert.Throws<ArgumentNullException>(() => userService.Register(userRegisterDTO));
            Assert.That(ex.Message, Is.EqualTo("All fields must not be null or empty"));
        }

        [Test]
        public void Register_UserRegisterDTOPropsAreNotNullOrEmpty_ReturnUserEntity()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO
            {
                Email = "marko@gmail.com",
                Username = "markos",
                Password = "password",
                Name = "Marko",
                Surname = "Spasenovski"
            };

            var user = new User
            {
                Email = "marko@gmail.com",
                Username = "markos",
                Password = "password",
                Role = Role.StandardUser.ToString(),
                Name = "Marko",
                Surname = "Spasenovski"
            };

            mockUserRepository.Setup(m => m.Insert(It.IsAny<User>())).Returns(user);

            var actual = userService.Register(userRegisterDTO);
            var expected = user;

            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(expected.Password, actual.Password);
            Assert.AreEqual(expected.Role, Role.StandardUser.ToString());
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Surname, actual.Surname);
        }

        [Test]
        public void UserExists_UserRegisterDTOIsNull_ReturnArgumentNullException()
        {
            UserRegisterDTO userRegisterDTO = null;
            var ex = Assert.Throws<ArgumentNullException>(() => userService.UserExists(userRegisterDTO));
            Assert.That(ex.Message, Is.EqualTo("UserRegisterDTO must not be null"));
        }

        [Test]
        public void UserExists_UsernameAndEmailAreNull_ReturnArgumentNullException()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO 
            {
                Username = null,
                Email = null
            };

            var ex = Assert.Throws<ArgumentNullException>(() => userService.UserExists(userRegisterDTO));
            Assert.That(ex.Message, Is.EqualTo("Username or email must not be null or empty"));
        }

        [Test]
        public void UserExists_UsernameAndEmailAreEmpty_ReturnArgumentNullException()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO
            {
                Username = string.Empty,
                Email = string.Empty
            };

            var ex = Assert.Throws<ArgumentNullException>(() => userService.UserExists(userRegisterDTO));
            Assert.That(ex.Message, Is.EqualTo("Username or email must not be null or empty"));
        }

        [Test]
        public void UserExists_UserFound_ReturnTrue()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO
            {
                Username = "markos",
                Email = null
            };

            mockUserRepository.Setup(m => m.UserExists(userRegisterDTO.Username, userRegisterDTO.Email)).Returns(true);

            var actual = userService.UserExists(userRegisterDTO);

            Assert.IsTrue(actual);
        }

        [Test]
        public void UserExists_UserNotFound_ReturnArgumentNullException()
        {
            UserRegisterDTO userRegisterDTO = new UserRegisterDTO
            {
                Username = null,
                Email = "marko@gmail.com"
            };

            mockUserRepository.Setup(m => m.UserExists(userRegisterDTO.Username, userRegisterDTO.Email)).Returns(false);

            var actual = userService.UserExists(userRegisterDTO);

            Assert.IsFalse(actual);
        }

        [Test]
        public void UpdateUser_UserIsNull_ReturnArgumentNullException()
        {
            User user = null;

            mockUserRepository.Setup(m => m.Update(user)).Throws<ArgumentNullException>();

            var ex = Assert.Throws<ArgumentNullException>(() => userService.UpdateUser(user));
            Assert.That(ex.Message, Is.EqualTo("Entity must not be null"));
        }

        [Test]
        public void UpdateUser_UserIsNotNull_ReturnUserEntity()
        {
            User user = new User { };

            mockUserRepository.Setup(m => m.Update(user)).Returns(user);

            var actual = userService.UpdateUser(user);

            Assert.IsNotNull(actual);
        }

        [Test]
        public void DeleteUser_UserIsNull_ReturnArgumentNullException()
        {
            User user = null;

            mockUserRepository.Setup(m => m.Delete(user)).Throws<ArgumentNullException>();

            var ex = Assert.Throws<ArgumentNullException>(() => userService.DeleteUser(user));
            Assert.That(ex.Message, Is.EqualTo("Entity must not be null"));
        }

        [Test]
        public void DeleteUser_UserIsNotNull_ReturnUserEntity()
        {
            User user = new User { };

            mockUserRepository.Setup(m => m.Delete(user)).Returns(user);

            var actual = userService.DeleteUser(user);

            Assert.IsNotNull(actual);
        }
    }
}
