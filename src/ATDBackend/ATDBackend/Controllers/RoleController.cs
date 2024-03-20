using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; //You know what this is...

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly AppDBContext _context;

        public RoleController(
            ILogger<AuthController> logger,
            IConfiguration configuration,
            AppDBContext context
        )
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("all")]
        public IActionResult getRoles() //REQUIRES AUTHENTICATION
        {
            return Ok(_context.Roles.ToList());
        }

        [HttpPost]
        public IActionResult addRole([FromBody] Role role)
        {
            try
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return StatusCode(500, (e));
            }
            return Ok(role);
        }
    }
}
