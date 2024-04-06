using System.Text.Json; //You know what this is
using ATDBackend.DTO;
using ATDBackend.Database.DBContexts;
using ATDBackend.Database.Models;
using ATDBackend.Security;
using Microsoft.AspNetCore.Mvc;

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;

        private readonly string[] _status = ["Pending", "Processing", "Shipped", "Delivered"];

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <remarks>
        /// This endpoint requires authentication. Only authenticated users can access this endpoint.
        /// This endpoint does not take any parameters and returns the orders of the authenticated user.
        /// </remarks>
        /// <returns>All orders of the authenticated user.</returns>
        [HttpGet]
        [CheckAuth]
        public IActionResult GetOrder()
        {
            if (HttpContext.Items["User"] is not User user)
            {
                return Unauthorized("No User");
            }
            else
            {
                var orders = _context.Orders.Where(o => o.UserId == user.Id).ToList();
                return Ok(orders);
            }
        }

        /// <summary>
        /// Create new order.
        /// </summary>
        /// <remarks>
        /// This endpoint requires authentication. Only authenticated users can access this endpoint.
        /// This endpoint requires a JSON object in the request body. The JSON object should contain the following fields:
        /// Email, PhoneNumber, Address
        /// The order does not take the products as parameters. Instead, it takes them directly from the authenticated user. Please make sure that the user's basket is up to date before calling this endpoint.
        /// </remarks>
        /// <param name="order"></param>
        /// <returns>The order object with a status code of 201</returns>
        [HttpPost]
        [CheckAuth]
        public IActionResult CreateOrder([FromBody] OrderDTO order)
        {
            //Check if order is valid
            if (HttpContext.Items["User"] is not User user)
            {
                return Unauthorized("No User");
            }
            if (!order.Email.Contains("@"))
            {
                return BadRequest("Invalid Email");
            }
            if (order.PhoneNumber.Length != 10)
            {
                return BadRequest("Invalid Phone Number");
            }
            if (order.Address.Length < 20)
            {
                return BadRequest("Invalid Address");
            }
            if (user.BasketJson is null)
            {
                return BadRequest("No Basket");
            }
            //Create basketSeeds
            List<BasketSeed>? basketSeeds = JsonSerializer.Deserialize<List<BasketSeed>>(
                user.BasketJson
            );
            float? totalOrderPrice = 0; //Order Price
            List<int> seedIds = [];
            if (basketSeeds is null)
            {
                return BadRequest("No Seeds in Basket or invalid basket");
            }
            foreach (var seedinBasket in basketSeeds)
            {
                var seed = _context.Seeds.Find(seedinBasket.Id);
                if (
                    seed is null
                    || seed.Is_active == false //seed isn't active
                    || seed.Stock < seedinBasket.Quantity //not enough stock
                    || seed.Price != seedinBasket.Price //price has changed
                    || seed.Price <= 0 //seed is free or negative price
                )
                {
                    return BadRequest("Invalid Seed in basket"); //If seed is not purchasable
                }
                else
                {
                    totalOrderPrice += seed.Price * seedinBasket.Quantity; //Set total order price

                    for (int i = 0; i < seedinBasket.Quantity; i++)
                    {
                        seedIds.Add(seed.Id);
                    }
                }
            }
            //All checks passed, create order
            if (totalOrderPrice <= 0 || totalOrderPrice > 1000000 || totalOrderPrice is null)
            {
                return BadRequest("Invalid Order Price");
            }

            var newOrder = new Order
            {
                SchoolId = user.SchoolId,
                UserId = user.Id,
                Address = order.Address,
                PhoneNumber = order.PhoneNumber,
                Email = order.Email,
                Status = _status[0],
                Price = (float)totalOrderPrice,
                Timestamp = DateTime.UtcNow,
                Seeds = seedIds,
                OrderDetails = JsonSerializer.Serialize(basketSeeds)
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetOrder), new { orderId = newOrder.Id }, newOrder);
        }

        /// <summary>
        /// Modify an order. ADMIN ONLY
        /// </summary>
        /// <remarks>
        /// This endpoint requires admin authentication. Only authenticated admins can access this endpoint.
        /// You can provide any of the following parameters in the request body:
        /// Address, PhoneNumber, Email, StatusId, StatusName
        /// You cannot provide both StatusId and StatusName at the same time. That will result in an error.
        /// </remarks>
        /// <param name="orderId">Order ID to modify</param>
        /// <param name="orderToModify">Modified order details</param>
        /// <returns></returns>
        [HttpPatch("{orderId}")]
        [CheckAuth("admin")]
        public IActionResult ModifyOrder(int orderId, [FromBody] OrderToModifyDTO orderToModify)
        {
            var order = _context.Orders.Find(orderId);
            if (order is null)
            {
                return NotFound("Order not found");
            }
            if (HttpContext.Items["User"] is not User user)
            {
                return Unauthorized("No User");
            }
            if (orderToModify == null)
            {
                return BadRequest("No Order to modify");
            }
            if (orderToModify.StatusId != null && orderToModify.StatusName != null)
            {
                return BadRequest("Can't have both StatusId and StatusName");
            }
            if (orderToModify.StatusId != null)
            {
                order.Status = _status[(int)orderToModify.StatusId];
            }
            if (orderToModify.StatusName != null)
            {
                if (!_status.Contains(orderToModify.StatusName))
                {
                    return BadRequest("Invalid Status Name");
                }
                else
                {
                    order.Status = orderToModify.StatusName;
                }
            }
            order.Address = orderToModify.Address ?? order.Address;
            order.PhoneNumber = orderToModify.PhoneNumber ?? order.PhoneNumber;
            order.Email = orderToModify.Email ?? order.Email;
            _context.SaveChanges();
            return Ok(order);
        }
    }
}
