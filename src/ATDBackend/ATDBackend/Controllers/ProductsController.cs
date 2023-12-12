using ATDBackend.Database.DBContexts;
using ATDBackend.Database.Models;
using ATDBackend.Security;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult AddProduct([FromBody] Seed seed)
        {
            _context.Seeds.Add(seed);
            _context.SaveChanges();
            return Ok();
        }
    }
}
