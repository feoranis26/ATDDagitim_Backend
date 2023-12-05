using ATDBackend.Security;
using Microsoft.AspNetCore.Mvc;

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        [HttpGet("login")]
        public IActionResult Login(string username, string pw)
        {
            Token token = TokenHandler.CreateToken(_configuration);
            return Ok(token);
        }
    }
}