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
    public class RoleController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;

        /// <summary>
        /// Get all roles. ADMIN ONLY
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [CheckAuth("Admin")]
        public IActionResult GetRoles() //REQUIRES AUTHENTICATION
        {
            return Ok(_context.Roles.ToList());
        }

        /// <summary>
        /// Add a new role. ADMIN ONLY
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckAuth("Admin")]
        public IActionResult AddRole([FromBody] Role role)
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
