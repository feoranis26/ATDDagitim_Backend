using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using Microsoft.AspNetCore.Mvc;
using ATDBackend.Utils;
using ATDBackend.Security.SessionSystem; //You know what this is...

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

        /// <summary>
        /// Login endpoint
        /// </summary>
        /// <param name="username">username of the user</param>
        /// <param name="password">plain text password of the user</param>
        /// <returns>Status code 200 and SetCookie header with the token.</returns>
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            if (username.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                return Unauthorized("invalidcredentials");
            }

            User? user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Hashed_PW))
            {
                return Unauthorized("invalidcredentials");
            }

            Session ses = SessionHandler.CreateSession(user.Id);

            Response.Cookies.Append(
                    "sid",
                    ses.SessionID,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None
                    }
                );


            Response.Cookies.Append(
                    "loggedin",
                    "true",
                    new CookieOptions
                    {
                        SameSite = SameSiteMode.None
                    }
                );

            return Ok();
        }

        [HttpPost("logout")]
        [RequireAuth(Permission.None)]
        public IActionResult Logout()
        {
            var user = (Database.Models.User?)HttpContext.Items["User"];

            Session? ses = SessionHandler.GetSessionByUserID(user.Id);

            if(ses != null) SessionHandler.RemoveSession(ses);


            Response.Cookies.Append(
                    "loggedin",
                    "false",
                    new CookieOptions
                    {
                        SameSite = SameSiteMode.None
                    }
                );

            return Ok();
        }


        [HttpGet]
        [RequireAuth(Permission.None)]
        public IActionResult CheckAuth() => Ok();
    }
}
