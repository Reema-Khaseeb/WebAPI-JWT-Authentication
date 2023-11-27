using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebAPIJWTAuthentication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("public")]
        public IActionResult GetPublicData()
        {
            return Ok("Welcome to the public endpoint!");
        }

        [HttpGet("protected")]
        public IActionResult GetProtectedData()
        {
            return Ok("Welcome to the protected endpoint!");
        }
    }
}
