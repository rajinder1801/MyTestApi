using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyApiTest.Interfaces;
using System;
using System.Net;

namespace MyApiTest.Controllers
{
    [ApiController]
    [Route("api/answers")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <returns>User</returns>
        [HttpGet("user")]
        public IActionResult Get()
        {
            try
            {
                var user = _userService.GetUser();
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
