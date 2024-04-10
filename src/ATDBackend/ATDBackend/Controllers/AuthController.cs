﻿using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using Microsoft.AspNetCore.Mvc;
using ATDBackend.Utils; //You know what this is...

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

            var User = _context.Users.FirstOrDefault(u => u.Username == username);

            if (User == null || !BCrypt.Net.BCrypt.Verify(password, User.Hashed_PW))
            {
                return Unauthorized("invalidcredentials");
            }


            // Create a token (JWT)
            Token token = TokenHandler.CreateToken(_configuration, User.Id);
            Response.Cookies.Append(
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
    }
}
