using Microsoft.AspNetCore.Mvc;
using RouteAppAPI.Models.DTO;
using RouteAppAPI.Services.Interfaces;

namespace RouteAppAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var auth = await _authService.LoginAsync(loginDto);

            if (auth == null)
                return Unauthorized(new { message = "Invalid email or password." });

            Response.Cookies.Append("refresh_token", auth.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = auth.RefreshTokenExpiry,
                Path = "/api/auth/refresh"
            });

            return Ok(new
            {
                access_token = auth.AccessToken,
                accessTokenExpiry = auth.AccessTokenExpiry
            });
        }
    }
}
