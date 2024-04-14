using System.Text.Json;
using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Modules;
using ATDBackend.Security; //login and register operation
using BCrypt.Net; //Hashing
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ATDBackend.Security.SessionSystem;

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register([FromBody] UserDto userDto) //Register user
        {
            if (!PatternVerifier.VerifyEmail(userDto.Email))
            {
                return BadRequest("Email must be real!");
            }
            if (userDto.Username.Length < 3 || userDto.Username.Length > 20)
            {
                return BadRequest("Username length must be between 3 and 20 characters.");
            }
            if (userDto.Password.Length < 8 || userDto.Password.Length > 30)
            {
                return BadRequest("Password length must be between 8 and 30 characters.");
            }
            var school = _context.Schools.Find(userDto.SchoolId);
            var role = _context.Roles.Find(userDto.RoleId);

            if (school == null || role == null)
            {
                return BadRequest("Invalid SchoolId or RoleId");
            }

            var user = new User
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                Email = userDto.Email,
                Phone_number = userDto.Phone_number,
                Hashed_PW = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                SchoolId = school.Id,
                Role = role,
                Register_date = DateTime.UtcNow,
                Username = userDto.Username
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // TODO: Add a function to send HTML as mail body

            /*
            MailSender
                .SendMail(
                    user.Email,
                    "Şehirbahçelerine Hoşgeldiniz!",
                    "ŞehirBahçeleri Ailesine Hoşgeldiniz!",
                    "<h1>Merhaba "
                        + user.Name
                        + " "
                        + user.Surname
                        + "</h1>"
                        + "<h2>ŞehirBahçeleri ailesine hoşgeldiniz. Artık <a href='https://sehirbahceleri.com.tr'>sitemizdeki</a> bütün özelliklerden faydalanabilirsiniz.</h3><br><p>Şehirbahçeleri</p>",
                    user.Name
                )
                .Wait();
            */
            //! FOR TESTING ONLY
            //return Created(nameof(GetUserDetails), user);//FOR TESTING ONLY REPLACE IN FULL APP. 

            //PRODUCTION:
            return Created();
        }


        [HttpGet]
        [RequireAuth(Permission.None)]
        public IActionResult GetSelf()
        {
            User? user = HttpContext.Items["User"] as User;

            if(user == null) return Unauthorized("nouser");

            return Ok(new
            {
                user.Username,
                user.Name,
                user.Surname,
                user.Email,
                user.Phone_number
            });
        }
    }
}
