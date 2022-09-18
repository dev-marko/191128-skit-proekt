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
    public class BadgeService : IBadgeService
    {
        private readonly IRepository<Badge> badgeRepository;
        private readonly IRepository<UserBadges> userBadgesRepository;
        private readonly IUserService userService;

        public BadgeService(IRepository<Badge> badgeRepository, IRepository<UserBadges> userBadgesRepository, IUserService userService)
        {
            this.badgeRepository = badgeRepository;
            this.userBadgesRepository = userBadgesRepository;
            this.userService = userService;
        }

        public Badge AddBadge(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(null, "Badge name must not be null");
            }

            Badge b = new Badge
            {
                Name = name
            };

            return badgeRepository.Insert(b);
        }

        public UserBadges AddBadgeToUser(UserBadgeDTO userBadgeDTO)
        {
            if(userBadgeDTO == null)
            {
                throw new ArgumentNullException(null, "UserBadgeDTO must not be null");
            }

            if(string.IsNullOrEmpty(userBadgeDTO.BadgeName))
            {
                throw new ArgumentNullException(null, "Badge name must not be null or empty");
            }

            if (string.IsNullOrEmpty(userBadgeDTO.Username))
            {
                throw new ArgumentNullException(null, "Username must not be null or empty");
            }

            Badge badge = FetchBadgeByName(userBadgeDTO.BadgeName); 

            if (badge == null)
            {
                throw new ArgumentNullException(null, "Badge not found");
            }

            User user = userService.FetchUserByUsername(userBadgeDTO.Username);

            if (user == null)
            {
                throw new ArgumentNullException(null, "User not found");
            }

            UserBadges userBadge = new UserBadges
            {
                Username = user.Username,
                User = user,
                BadgeId = badge.Id,
                Badge = badge
            };

            return userBadgesRepository.Insert(userBadge);
        }

        public Badge FetchBadgeByName(string name)
        {
            return badgeRepository.FetchAll().SingleOrDefault(e => e.Name.Equals(name));
        }
    }
}
