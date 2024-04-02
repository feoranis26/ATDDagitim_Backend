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
    public class BasketController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;

        [HttpGet]
        [CheckAuth]
        public IActionResult GetBasket() //REQUIRES AUTHENTICATION
        {
            var user = HttpContext.Items["User"];

            if (user == null)
            {
                return BadRequest("Invalid User");
            }
            else
            {
                return Ok(user);
            }
            return Ok();
        }
    }
}
