using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using Microsoft.AspNetCore.Mvc; //You know what this is...

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolsController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;

        /// <summary>
        /// Add a new school. ADMIN ONLY
        /// </summary>
        /// <param name="school"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckAuth("Admin")]
        public IActionResult AddSchool([FromBody] School school) //REQUIRES AUTHENTICATION
        {
            _context.Schools.Add(school);
            _context.SaveChanges();
            return Ok(school);
        }

        /// <summary>
        /// Get all schools.
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public IActionResult GetSchools() //REQUIRES AUTHENTICATION
        {
            return Ok(_context.Schools.ToList());
        }
    }
}
