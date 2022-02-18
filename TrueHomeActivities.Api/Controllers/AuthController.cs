using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrueHomeActivities.Api.Auth;
using TrueHomeActivities.Api.Models;

namespace TrueHomeActivities.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationAndAuthorization _authenticationAndAuthorization;

        public AuthController(AuthenticationAndAuthorization authenticationAndAuthorization)
        {
            _authenticationAndAuthorization = authenticationAndAuthorization;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] User request)
        {
            string? token = _authenticationAndAuthorization.GrantAccess(request);

            if (token == null)
            {
                return NotFound("User Not Found");
            }

            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User request)
        {
            UsersRepository.Users.Add(request);

            return Ok("Registered");
        }
    }
}
