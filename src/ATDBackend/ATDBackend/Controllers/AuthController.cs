using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using Microsoft.AspNetCore.Mvc; //You know what this is...

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
                        "Bearer " + token.AccessToken,
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

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserDto userDto) //Register user
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
                Surname = userDto.Surname,
                Email = userDto.Email,
                Phone_number = userDto.Phone_number,
                Hashed_PW = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                School_id = school,
                Role = role,
                Register_date = DateTime.UtcNow.AddHours(3),
                Username = userDto.Username,
                BasketJson = "[]"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }
    }
}
