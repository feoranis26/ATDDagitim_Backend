using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
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
        public IActionResult AddUser([FromBody] UserDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                Phone_number = userDto.Phone_number,
                Hashed_PW = userDto.Hashed_PW,
                School_id = _context.Schools.Find(userDto.SchoolId),
                Role_id = _context.Roles.Find(userDto.RoleId),
                Register_date = DateTime.UtcNow.AddHours(3),
                Username = userDto.Username
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }
    }
}
