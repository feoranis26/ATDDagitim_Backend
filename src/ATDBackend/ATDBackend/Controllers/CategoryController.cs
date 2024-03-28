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
    public class CategoryController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;

        [HttpGet("all")]
        public IActionResult GetCategories()
        {
            return Ok(_context.Categories.ToList());
        }

        [HttpPost]
        [CheckAuth("Admin")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok(category);
        }

        [HttpGet]
        public IActionResult getCategory(int categoryID)
        {
            return Ok(_context.Categories.Where(x => x.Id == categoryID).FirstOrDefault());
        }
    }
}
