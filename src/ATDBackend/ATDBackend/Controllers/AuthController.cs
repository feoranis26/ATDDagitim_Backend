using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using ATDBackend.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc; //You know what this is...
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly AppDBContext _context;

        public AuthController(
            ILogger<AuthController> logger,
            IConfiguration configuration,
            AppDBContext context
        )
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("login")]
        public IActionResult Login(string username, string pw)
        {
            var User = _context.Users.FirstOrDefault(u => u.Username == username);
            if (User == null)
            {
                return Unauthorized("Invalid Username");
            }
            Token token = TokenHandler.CreateToken(_configuration, User.Id);
            return Ok(token);
        }
    }
}
