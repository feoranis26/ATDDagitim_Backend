using System.IdentityModel.Tokens.Jwt;
using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc; //You know what this is...

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly AppDBContext _context;

        public UsersController(
            ILogger<AuthController> logger,
            IConfiguration configuration,
            AppDBContext context
        )
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            var school = _context.Schools.Find(userDto.SchoolId);
            var role = _context.Roles.Find(userDto.RoleId);

            if (school == null || role == null)
            {
                return BadRequest("Invalid SchoolId or RoleId");
            }

            if (userDto.Username.Length < 3 || userDto.Username.Length > 20)
            {
                return BadRequest("Username length must be between 3 and 20 characters.");
            }
            if (userDto.Email.Length < 3 || userDto.Email.Length > 50)
            {
                return BadRequest("Email length must be between 3 and 50 characters.");
            }
            if (!userDto.Email.Contains("@"))
            {
                return BadRequest("Email must be real. Duuh!");
            }
            if (userDto.Password.Length < 8 || userDto.Password.Length > 30)
            {
                return BadRequest("Password length must be between 8 and 30 characters.");
            }

            var user = new User
            {
                Name = userDto.Name,
                surname = userDto.Surname,
                Email = userDto.Email,
                Phone_number = userDto.Phone_number,
                Hashed_PW = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                School_id = school,
                Role_id = role,
                Register_date = DateTime.UtcNow.AddHours(3),
                Username = userDto.Username
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto userDto) //ONLY FOR TESTING PURPOSES
        {

            return Ok("TEST");

            var user = _context.Users.SingleOrDefault(u => u.Email == userDto.Email);

            if (user == null)
            {
                return BadRequest("Invalid email or password.");
            }

            // Verify the password
            bool validPassword = BCrypt.Net.BCrypt.Verify(userDto.Password, user.Hashed_PW);

            if (!validPassword)
            {
                return Unauthorized("Invalid email or password.");
            }
            var tokenHandler = new Token();
            var createdToken = TokenHandler.CreateToken(_configuration, user.Id);

            // If we reach this point, the user is authenticated
            // Here you might generate and return a JWT for the user, or just return a success message
            var returnObject = new { token = createdToken.AccessToken, };
            return Ok(returnObject);
        }

        
    }
}
