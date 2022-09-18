using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        private readonly IBadgeService badgeService;

        public BadgeController(IBadgeService badgeService)
        {
            this.badgeService = badgeService;
        }

        [HttpPost]
        public IActionResult AddBadge([FromBody] string name)
        {
            return Ok(badgeService.AddBadge(name));
        }

        [HttpPost("add-to-user")]
        public IActionResult AddBadgeToUser([FromBody] UserBadgeDTO userBadgeDTO)
        {
            return Ok(badgeService.AddBadgeToUser(userBadgeDTO));
        }
    }
}
