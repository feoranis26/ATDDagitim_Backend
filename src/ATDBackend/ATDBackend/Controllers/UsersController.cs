using System.Text.Json;
using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Modules;
using ATDBackend.Security; //login and register operation
using BCrypt.Net; //Hashing
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;

        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register([FromBody] UserDto userDto) //Register user
        {
            if (!PatternVerifier.VerifyEmail(userDto.Email))
            {
                return BadRequest("Email must be real!");
            }
            if (userDto.Username.Length < 3 || userDto.Username.Length > 20)
            {
                return BadRequest("Username length must be between 3 and 20 characters.");
            }
            if (userDto.Password.Length < 8 || userDto.Password.Length > 30)
            {
                return BadRequest("Password length must be between 8 and 30 characters.");
            }
            var school = _context.Schools.Find(userDto.SchoolId);
            var role = _context.Roles.Find(userDto.RoleId);

            if (school == null || role == null)
            {
                return BadRequest("Invalid SchoolId or RoleId");
            }

            var user = new User
            {
                Name = userDto.Name,
                Surname = userDto.Surname,
                Email = userDto.Email,
                Phone_number = userDto.Phone_number,
                Hashed_PW = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                SchoolId = school.Id,
                Role = role,
                Register_date = DateTime.UtcNow,
                Username = userDto.Username,
                BasketJson = "[]"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            // TODO: Add a function to send HTML as mail body

            /*
            MailSender
                .SendMail(
                    user.Email,
                    "Şehirbahçelerine Hoşgeldiniz!",
                    "ŞehirBahçeleri Ailesine Hoşgeldiniz!",
                    "<h1>Merhaba "
                        + user.Name
                        + " "
                        + user.Surname
                        + "</h1>"
                        + "<h2>ŞehirBahçeleri ailesine hoşgeldiniz. Artık <a href='https://sehirbahceleri.com.tr'>sitemizdeki</a> bütün özelliklerden faydalanabilirsiniz.</h3><br><p>Şehirbahçeleri</p>",
                    user.Name
                )
                .Wait();
            */
            //! FOR TESTING ONLY
            return Created(nameof(GetUserDetails), user);//FOR TESTING ONLY REPLACE IN FULL APP. 

            //PRODUCTION:
            //return Created();
        }

        /// <summary>
        /// Send a mail TESTING PURPOSES. ADMIN ONLY
        /// </summary>
        /// <returns></returns>
        [HttpGet("sendMail")]
        [RequireAuth(Permission.PERMISSION_ADMIN)]
        public IActionResult MailSendTest()
        {
            bool suc = MailModule.SendMail("sehirbahceleri@gmail.com", "Test subject", "Test body");
            return suc ? Ok("Mail sent") : StatusCode(500, "An error occured while sending mail");
        }

        /// <summary>
        /// Get user details. USER ONLY
        /// </summary>
        /// <returns>The user object with all fields.</returns>
        [HttpGet("details")]
        [RequireAuth(Permission.USER_SELF_READ)]
        public IActionResult GetUserDetails()
        {
            var user = HttpContext.Items["User"];
            Console.WriteLine("USER DETAILS: " + user);
            return Ok(user);
        }

        /// <summary>
        /// Get the basket of the user. SCHOOL USER ONLY
        /// </summary>
        /// <returns></returns>
        [HttpGet("basket")]
        [RequireAuth(Permission.USER_SELF_BASKET)]
        public IActionResult GetBasket()
        {
            if (HttpContext.Items["User"] is not User user)
            {
                return BadRequest("User not found.");
            }
            var dbUser = _context.Users.Find(user.Id);
            if (dbUser is not null)
            {
                var basket = dbUser.BasketJson ?? "[]";
                //Also checks everything in basket
                List<BasketSeed>? deserializedBasket = JsonSerializer.Deserialize<List<BasketSeed>>(
                    basket
                );
                if (deserializedBasket is null)
                {
                    return Ok(new List<BasketSeed>());
                }
                List<BasketSeed> finalBasket = deserializedBasket;
                foreach (var product in deserializedBasket)
                {
                    var tempProduct = _context.Seeds.Find(product.Id);
                    if (tempProduct is null)
                    {
                        finalBasket.Remove(product);
                        continue;
                    }
                    if (tempProduct.Stock < product.Quantity)
                    {
                        deserializedBasket.FirstOrDefault(x => x.Id == product.Id).Quantity =
                            tempProduct.Stock;
                    }
                    if (tempProduct.Is_active == false || tempProduct.Price <= 0)
                    {
                        finalBasket.Remove(product);
                    }
                }
                dbUser.BasketJson = JsonSerializer.Serialize(finalBasket);
                _context.SaveChanges();
                return Ok(finalBasket);
            }
            return BadRequest();
        }

        /// <summary>
        /// Add a product to the basket. USER ONLY
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost("basket")]
        [RequireAuth(Permission.USER_SELF_BASKET)]
        public IActionResult AddToBasket(int productId, int? quantity = 1)
        {
            var tempProduct = _context.Seeds.Find(productId); //Find the product
            if (tempProduct is null)
            {
                return NotFound("Product not found."); //If product not found return BadRequest
            }
            if (tempProduct.Stock < quantity)
            {
                return StatusCode(409, "Not enough stock."); //Don't request more product than the stock
            }
            if (tempProduct.Is_active == false || tempProduct.Price <= 0)
            {
                return BadRequest("Product can't be purchased."); //Don't request inactive products
            }
            var basketSeed = new BasketSeed
            {
                Id = tempProduct.Id,
                Name = tempProduct.Name,
                Price = tempProduct.Price,
                Stock = tempProduct.Stock,
                CategoryId = tempProduct.CategoryId,
                Quantity = quantity
            };
            if (HttpContext.Items["User"] is not User user)
            {
                return BadRequest("User not found.");
            }
            var dbUser = _context.Users.Find(user.Id);
            if (dbUser is not null)
            {
                _context.Users.Update(dbUser);
                var basket =
                    JsonSerializer.Deserialize<List<BasketSeed>>(dbUser.BasketJson ?? "[]")
                    ?? new List<BasketSeed>();
                var alreadyInBasket = basket.FirstOrDefault(x => x.Id == productId);

                var newBasket = basket.ToList();

                if (alreadyInBasket is null)
                {
                    dbUser.BasketJson ??= JsonSerializer.Serialize(new List<BasketSeed>());
                    newBasket.Add(basketSeed);
                    dbUser.BasketJson = JsonSerializer.Serialize(newBasket);
                }
                else
                {
                    alreadyInBasket.Quantity =
                        quantity == 1 ? alreadyInBasket.Quantity + quantity : quantity;

                    newBasket.FirstOrDefault(x => x.Id == productId).Quantity =
                        alreadyInBasket.Quantity;
                    dbUser.BasketJson = JsonSerializer.Serialize(newBasket);
                }

                _context.SaveChanges();
                return Ok(dbUser.BasketJson);
            }
            return StatusCode(500, "Houston, we have a problem.");
        }

        /// <summary>
        /// Remove a product from the basket. USER ONLY
        /// </summary>
        /// <param name="ProductId">Id of the product to remove.</param>
        /// <param name="Quantity">How many products to remove.</param>
        /// <returns></returns>
        [HttpDelete("basket")]
        [RequireAuth(Permission.USER_SELF_BASKET)]
        public IActionResult RemoveFromBasket(int ProductId, int? Quantity = 0)
        {
            if (HttpContext.Items["User"] is not User user)
            {
                return BadRequest("User not found.");
            }
            var dbUser = _context.Users.Find(user.Id);
            if (dbUser is not null)
            {
                var basket =
                    JsonSerializer.Deserialize<List<BasketSeed>>(dbUser.BasketJson ?? "[]")
                    ?? new List<BasketSeed>();
                var alreadyInBasket = basket.FirstOrDefault(x => x.Id == ProductId);
                if (alreadyInBasket is null)
                {
                    return NotFound("Product not found in basket.");
                }
                var newBasket = basket.ToList();
                if (Quantity == 0 || Quantity >= alreadyInBasket.Quantity)
                {
                    newBasket.Remove(alreadyInBasket);
                }
                else
                {
                    alreadyInBasket.Quantity -= Quantity;
                    newBasket.FirstOrDefault(x => x.Id == ProductId).Quantity =
                        alreadyInBasket.Quantity;
                }
                dbUser.BasketJson = JsonSerializer.Serialize(newBasket);
                _context.SaveChanges();
                return Ok(dbUser.BasketJson);
            }
            return StatusCode(500, "Houston, we have a problem.");
        }

        /// <summary>
        /// Delete all products from the basket. USER ONLY
        /// </summary>
        /// <returns></returns>
        [HttpDelete("basket/all")]
        [RequireAuth(Permission.USER_SELF_BASKET)]
        public IActionResult DeleteBasket()
        {
            if (HttpContext.Items["User"] is not User user)
            {
                return BadRequest("User not found.");
            }
            var dbUser = _context.Users.Find(user.Id);
            if (dbUser is not null)
            {
                dbUser.BasketJson = JsonSerializer.Serialize(new List<BasketSeed>());
                _context.SaveChanges();
                return Ok(dbUser.BasketJson);
            }
            return StatusCode(500, "Houston, we have a problem.");
        }
    }
}
