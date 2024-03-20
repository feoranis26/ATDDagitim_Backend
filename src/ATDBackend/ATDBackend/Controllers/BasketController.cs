using System.Security.Claims;
using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using Microsoft.AspNetCore.Authorization; //You know what this is...
using Microsoft.AspNetCore.Mvc;

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly AppDBContext _context;

        public BasketController(
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
        [Authorize]
        public IActionResult GetBasket() //REQUIRES AUTHENTICATION
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            int userId;
            if (!int.TryParse(userIdClaim.Value, out userId))
            {
                return BadRequest("Invalid UserId");
            }
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return BadRequest("Invalid UserId");
            }
            return Ok();
        }
    }
}
