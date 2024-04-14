using ATDBackend.Database.DBContexts;
using ATDBackend.Database.Models;
using ATDBackend.Security.SessionSystem;
using ATDBackend.Security;
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
        [RequireAuth(Permission.SCHOOL_SELF_PURCHASEPRODUCT)]
        public IActionResult GetBasket()
        {
            return Ok();
        }

        [HttpPost]
        [RequireAuth(Permission.SCHOOL_SELF_PURCHASEPRODUCT)]
        public IActionResult AddToBasket(int seedId, int quantity)
        {
            return Ok();
        }

        [HttpDelete]
        [RequireAuth(Permission.SCHOOL_SELF_PURCHASEPRODUCT)]
        public IActionResult DeleteSingleFromBasket(int seedId)
        {
            return Ok();
        }

        [HttpDelete("all")]
        [RequireAuth(Permission.SCHOOL_SELF_PURCHASEPRODUCT)]
        public IActionResult DeleteAllFromBasket()
        {
            return Ok();
        }

        [HttpPost("purchase")]
        [RequireAuth(Permission.SCHOOL_SELF_PURCHASEPRODUCT)]
        public IActionResult PurchaseBasket(/* CONTACT INFO ARGS */)
        {
            return Ok();
        }
    }
}
