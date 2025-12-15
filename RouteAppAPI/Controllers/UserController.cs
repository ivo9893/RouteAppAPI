using Microsoft.AspNetCore.Mvc;
using RouteAppAPI.Models.DTO;
using RouteAppAPI.Services.Interfaces;

namespace RouteAppAPI.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.RegisterUser(userDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(new
            {
                message = result.Message
            });
        }
    }
}
