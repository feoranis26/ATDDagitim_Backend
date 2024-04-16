using Microsoft.AspNetCore.Mvc; //You know what this is...
//controllerupdate test

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Controller(ILogger<AuthController> logger, IConfiguration configuration)
        : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;

        /// <summary>
        /// Testing uptime
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDefault()
        {
            return Ok("Hi!");
        }
    
        /// <summary>
        /// Testing uptime with POST
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PostDefault()
        {
            return Ok("Test!");
        }
    }
}
