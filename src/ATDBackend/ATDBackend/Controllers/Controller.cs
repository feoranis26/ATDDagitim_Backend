using Microsoft.AspNetCore.Mvc; //You know what this is...

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller(ILogger<AuthController> logger, IConfiguration configuration)
        : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;

        [HttpGet]
        public IActionResult GetDefault()
        {
            return Ok("Hi!");
        }

        [HttpPost]
        public IActionResult PostDefault()
        {
            return Ok("Test!");
        }
    }
}
