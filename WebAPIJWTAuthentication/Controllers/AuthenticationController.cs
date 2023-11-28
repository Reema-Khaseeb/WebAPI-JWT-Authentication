using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebAPIJWTAuthentication.Models;

namespace WebAPIJWTAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtTokenGenerator _tokenGenerator;

        public AuthenticationController(JwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginCredentials loginCredentials)
        {
            // Validate user credentials
            if (IsValidUser(loginCredentials))
            {
                // If valid, generate and return a JWT token
                var token = _tokenGenerator.GenerateToken(loginCredentials);
                
                return Ok(new { Token = token });
            }

            // If invalid credentials, return an unauthorized response
            return Unauthorized(new { Message = "Invalid username or password" });
        }

        public bool IsValidUser(LoginCredentials loginCredentials)
        {
            var validUsers = new Dictionary<string, string>
            {
                {"user1", "password1"},
                {"user2", "password2"}
            };

            // Check if the provided username exists and the password matches
            return validUsers.TryGetValue(
                loginCredentials.Username, out var expectedPassword) &&
                loginCredentials.Password == expectedPassword;
        }
    }
}
