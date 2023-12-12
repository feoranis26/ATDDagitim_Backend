using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using Microsoft.AspNetCore.Mvc; //You know what this is...

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly AppDBContext _context;

        public ProductsController(
            ILogger<AuthController> logger,
            IConfiguration configuration,
            AppDBContext context
        )
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_context.Seeds);
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] SeedDto seedDto)
        {
            var category = _context.Categories.Find(seedDto.CategoryId);
            var user = _context.Users.Find(seedDto.UserId);

            if (category == null || user == null)
            {
                return BadRequest("Invalid CategoryId or UserId");
            }

            var seed = new Seed
            {
                Name = seedDto.Name,
                Category = category,
                Description = seedDto.Description,
                User_id = user,
                Stock = seedDto.Stock,
                Date_added = DateTime.UtcNow.AddHours(3),
                Price = seedDto.Price,
                Is_active = seedDto.Is_active,
                Image = seedDto.Image
            };

            _context.Seeds.Add(seed);
            _context.SaveChanges();

            return Ok();
        }
    }
}
