using FloraEducationAPI.Domain.DTO.Authentication;
using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FloraEducationAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Login([Bind("Username, Password")] UserLoginDTO userLoginDTO)
        {
            var user = userService.Authenticate(userLoginDTO);

            if (user != null)
            {
                return Ok(user.Username);
            }

            return NotFound("User not found");
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            
            if (userService.UserExists(userRegisterDTO))
            {
                return BadRequest("User already exists");
            }

            return Ok(userService.Register(userRegisterDTO));
        }

        [HttpGet("me")]
        public IActionResult GetCurrentlyLoggedInUser([FromQuery] string username)
        {
            var user = userService.FetchUserByUsername(username);
            return Ok(user);
        }
    }
}
