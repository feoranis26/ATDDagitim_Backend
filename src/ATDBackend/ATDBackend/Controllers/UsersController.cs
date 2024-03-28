using System;
using System.IdentityModel.Tokens.Jwt;
using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using BCrypt.Net;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Mvc; //You know what this is...
using Newtonsoft.Json.Linq;

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

        [HttpGet]
        public IActionResult getAllUsers()
        {
            return Ok(_context.Users.ToList());
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

            var user = new Database.Models.User
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

            static async Task RunAsync()
            {
                MailjetClient client = new MailjetClient(
                    Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"),
                    Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE")
                );
                MailjetRequest request = new MailjetRequest { Resource = Send.Resource, }
                    .Property(Send.SandboxMode, "true")
                    .Property(
                        Send.Messages,
                        new JArray
                        {
                            new JObject
                            {
                                {
                                    "From",
                                    new JArray
                                    {
                                        new JObject
                                        {
                                            { "Email", "pilot@mailjet.com" },
                                            { "Name", "Your Mailjet Pilot" }
                                        }
                                    }
                                },
                                {
                                    "HTMLPart",
                                    "<h3>Dear passenger, welcome to Mailjet!</h3><br />May the delivery force be with you!"
                                },
                                { "Subject", "Your email flight plan!" },
                                {
                                    "TextPart",
                                    "Dear passenger, welcome to Mailjet! May the delivery force be with you!"
                                },
                                {
                                    "To",
                                    new JArray
                                    {
                                        new JObject
                                        {
                                            { "Email", "sehirbahceleri@gmail.com" },
                                            { "Name", "sehirbahceleri" }
                                        }
                                    }
                                }
                            }
                        }
                    );
                MailjetResponse response = await client.PostAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(
                        string.Format(
                            "Total: {0}, Count: {1}\n",
                            response.GetTotal(),
                            response.GetCount()
                        )
                    );
                    Console.WriteLine(response.GetData());
                }
                else
                {
                    Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                    Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                    Console.WriteLine(response.GetData());
                    Console.WriteLine(
                        string.Format("ErrorMessage: {0}\n", response.GetErrorMessage())
                    );
                }
            }

            RunAsync().Wait();

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
