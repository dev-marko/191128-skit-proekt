using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Interfaces
{
    public interface IBadgeService
    {
        Badge AddBadge(string name);
        UserBadges AddBadgeToUser(UserBadgeDTO userBadgeDTO);
        Badge FetchBadgeByName(string name);
    }
}
