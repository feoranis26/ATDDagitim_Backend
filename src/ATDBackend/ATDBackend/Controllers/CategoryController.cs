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
    public class CategoryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly AppDBContext _context;

        public CategoryController(
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
        public IActionResult getCategories() //REQUIRES AUTHENTICATION
        {
            return Ok(_context.Categories.ToList());
        }

        [HttpPost]
        public IActionResult addCategory([FromBody] Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                
            }
            return Ok(category);
        }

        [HttpGet]
        public IActionResult getCategory(int categoryID)
        {
            return Ok(_context.Categories.Where(x => x.Id == categoryID).FirstOrDefault());
        }
    }
}
