using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using Microsoft.AspNetCore.Mvc;
using ATDBackend.Security.SessionSystem;
using ATDBackend.Migrations; //You know what this is...

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
        [RequireAuth(Permission.SCHOOL_GLOBAL_CREATE)]
        public IActionResult AddSchool([FromBody] School school) //REQUIRES AUTHENTICATION
        {
            school.Orders = "[]";
            _context.Schools.Add(school);
            _context.SaveChanges();
            return Ok(school);
        }

        /// <summary>
        /// Get all schools.
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [RequireAuth(Permission.SCHOOL_GLOBAL_READ)]
        public IActionResult GetSchools() //REQUIRES AUTHENTICATION
        {
            return Ok(_context.Schools.ToList());
        }

        [HttpGet]
        [RequireAuth(Permission.SCHOOL_SELF_READ)]
        public IActionResult GetSelfSchool() //REQUIRES AUTHENTICATION
        {
            if (HttpContext.Items["User"] is not User user) return Unauthorized("nouser");

            School? school = _context.Schools.Find(user.SchoolId);
            if (school == null) return NotFound("noschool");

            return Ok(new
            {
                school.Name,
                school.Credit
            });
        }
    }
}
