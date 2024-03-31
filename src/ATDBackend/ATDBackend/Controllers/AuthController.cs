using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; //You know what this is...
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var User = _context.Users.FirstOrDefault(u => u.Username == username);
            if (User == null)
            {
                return Unauthorized("Invalid Username");
            }
            if (!BCrypt.Net.BCrypt.Verify(password, User.Hashed_PW))
            {
                return Unauthorized("Invalid Password");
            }
            if (User != null && BCrypt.Net.BCrypt.Verify(password, User.Hashed_PW))
            {
                // Create a token (JWT)
                Token token = TokenHandler.CreateToken(_configuration, User.Id);
                Response
                    .Cookies
                    .Append(
                        "token",
                        "Bearer" + token.AccessToken,
                        new CookieOptions
                        {
                            Expires = DateTime.Now.AddMinutes(45),
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.None
                        }
                    );
                return Ok(token);
            }
            return StatusCode(500, "Internal Server Error");
        }
    }
}
