using FloraEducationAPI.Domain.DTO.Authentication;
using FloraEducationAPI.Domain.Enumerations;
using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace FloraEducationAPI.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public List<User> FetchAllUsers()
        {
            return userRepository.FetchAllUsers().ToList();
        }

        public User FetchUserByEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(null, "Email must not be null or empty");
            }

            return userRepository.FetchUserByEmail(email);
        }

        public User FetchUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(null, "Username must not be null or empty");
            }

            return userRepository.FetchUserByUsername(username);
        }

        public User UpdateUser(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(null, "Entity must not be null");
            }

            return userRepository.Update(entity);
        }

        public User DeleteUser(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(null, "Entity must not be null");
            }

            return userRepository.Delete(entity);
        }

        public User Authenticate(UserLoginDTO userLoginDTO)
        {
            if (userLoginDTO == null)
            {
                throw new ArgumentNullException(null, "UserLoginDTO must not be null");
            }

            if (string.IsNullOrEmpty(userLoginDTO.Username))
            {
                throw new ArgumentNullException(null, "Username must not be null or empty");
            }

            if (string.IsNullOrEmpty(userLoginDTO.Password))
            {
                throw new ArgumentNullException(null, "Password must not be null or empty");
            }

            return userRepository.VerifyUserCredentials(userLoginDTO.Username, userLoginDTO.Password);
        }

        public User Register(UserRegisterDTO userRegisterDTO)
        {
            if (userRegisterDTO == null)
            {
                throw new ArgumentNullException(null, "UserRegisterDTO must not be null");
            }

            string[] fields = new string[] { userRegisterDTO.Email, userRegisterDTO.Username, userRegisterDTO.Password, userRegisterDTO.Name, userRegisterDTO.Surname };
            bool anyNull = fields.Any(f => string.IsNullOrEmpty(f));

            if (anyNull)
            {
                throw new ArgumentNullException(null, "All fields must not be null or empty");
            }

            User user = new User
            {
                Email = userRegisterDTO.Email,
                Username = userRegisterDTO.Username,
                Password = BC.HashPassword(userRegisterDTO.Password),
                Role = Role.StandardUser.ToString(),  // Every user by default is a standard user
                Name = userRegisterDTO.Name,
                Surname = userRegisterDTO.Surname
            };

            return userRepository.Insert(user);
        }

        public bool UserExists(UserRegisterDTO userRegisterDTO)
        {
            if (userRegisterDTO == null)
            {
                throw new ArgumentNullException(null, "UserRegisterDTO must not be null");
            }

            if (string.IsNullOrEmpty(userRegisterDTO.Username) && string.IsNullOrEmpty(userRegisterDTO.Email))
            {
                throw new ArgumentNullException(null, "Username or email must not be null or empty");
            }

            return userRepository.UserExists(userRegisterDTO.Username, userRegisterDTO.Email);
        }
    }
}
