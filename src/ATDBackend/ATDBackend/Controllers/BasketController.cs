using ATDBackend.Database.DBContexts;
using ATDBackend.Database.Models;
using ATDBackend.Security.SessionSystem;
using ATDBackend.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            int? userId = (HttpContext.Items["User"] as User)?.Id;
            if (userId == null) return Unauthorized("nouser");

            var res =
                _context.Users.Where(x => x.Id == userId)
                .Include(x => x.BasketSeeds).ThenInclude(x => x.Seed).Select(x => new
                {
                    seeds = x.BasketSeeds.Select(x => new
                    {
                        id = x.Seed.Id,
                        name = x.Seed.Name,
                        price = x.Seed.Price,
                        quantity = x.Quantity
                    })
                }).FirstOrDefault();

            return Ok(res);
        }

        [HttpPost]
        [RequireAuth(Permission.SCHOOL_SELF_PURCHASEPRODUCT)]
        public IActionResult AddToBasket(int seedId, int quantity)
        {
            int? userId = (HttpContext.Items["User"] as User)?.Id;
            if (userId == null) return Unauthorized("nouser");

            if(_context.Seeds.Find(seedId) == null) return NotFound("seednotfound");
            if(quantity <= 0 || quantity > 200) return BadRequest("invalidquantity");



            User? user = _context.Users.Where(x => x.Id == userId).Include(x => x.BasketSeeds).FirstOrDefault();
            if(user == null) return Unauthorized("nouser");

            user.BasketSeeds.Add(new BasketSeed()
            {
                Quantity = quantity,
                SeedId = seedId,
                UserId = userId.Value
            });
            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [RequireAuth(Permission.SCHOOL_SELF_PURCHASEPRODUCT)]
        public IActionResult DeleteSingleFromBasket(int seedId)
        {
            int? userId = (HttpContext.Items["User"] as User)?.Id;
            if (userId == null) return Unauthorized("nouser");

            if (_context.Seeds.Find(seedId) == null) return NotFound("seednotfound");



            User? user = _context.Users.Where(x => x.Id == userId).Include(x => x.BasketSeeds).FirstOrDefault();
            if (user == null) return Unauthorized("nouser");

            BasketSeed? bs = user.BasketSeeds.Where(x => x.SeedId == seedId).FirstOrDefault();
            if(bs == null) return NotFound("basketseednotfound");

            bool s = user.BasketSeeds.Remove(bs);

            if (!s) return StatusCode(500);

            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("all")]
        [RequireAuth(Permission.SCHOOL_SELF_PURCHASEPRODUCT)]
        public IActionResult DeleteAllFromBasket()
        {
            int? userId = (HttpContext.Items["User"] as User)?.Id;
            if (userId == null) return Unauthorized("nouser");




            User? user = _context.Users.Where(x => x.Id == userId).Include(x => x.BasketSeeds).FirstOrDefault();
            if (user == null) return Unauthorized("nouser");


            user.BasketSeeds.Clear();


            _context.Users.Update(user);
            _context.SaveChanges();

            return Ok();
        }
    }
}
