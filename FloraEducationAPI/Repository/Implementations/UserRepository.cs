using FloraEducationAPI.Context;
using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using BC=BCrypt.Net.BCrypt;

namespace FloraEducationAPI.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        public FloraEducationDbContext context { get; set; }
        public DbSet<User> entities { get; set; }

        public UserRepository(FloraEducationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<User>();
        }

        public User Delete(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("User object is null");
            }

            var e = entities.Remove(entity).Entity;
            context.SaveChanges();
            return e;
        }

        public IEnumerable<User> FetchAllUsers()
        {
            return entities.AsEnumerable();
        }

        public User FetchUserByEmail(string email)
        {
            return entities.SingleOrDefault(e => e.Email == email);
        }

        public User FetchUserByUsername(string username)
        {
            return entities
                .Include(e => e.LikedPlants)
                .Include("LikedPlants.Plant")
                .Include(e => e.Badges)
                .Include("Badges.Badge")
                .SingleOrDefault(e => e.Username == username);
        }

        public User VerifyUserCredentials(string username, string password)
        {
            User user = FetchUserByUsername(username);
            
            if (user == null || !BC.Verify(password, user.Password))
            {
                return null;
            }

            return user;
        }

        public User Insert(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("User object is null");
            }

            var e = entities.Add(entity).Entity;
            context.SaveChanges();
            return e;
        }

        public User Update(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("User object is null");
            }

            var e = entities.Update(entity).Entity;
            context.SaveChanges();
            return e;
        }

        public bool UserExists(string username, string email)
        {
            return (FetchUserByUsername(username) != null) && (FetchUserByEmail(email) != null);
        }
    }
}
