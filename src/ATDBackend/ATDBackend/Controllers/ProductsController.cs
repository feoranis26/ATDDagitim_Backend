﻿using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;

        /// <summary>
        /// Add a new product. ADMIN ONLY
        /// </summary>
        /// <param name="seedDto"></param>
        /// <returns></returns>
        [HttpPost]
        [RequireAuth(Permission.PRODUCT_CREATE)]
        public IActionResult AddProduct([FromBody] SeedDto seedDto) //REQUIRES AUTHENTICATION
        {
            var category = _context.Categories.Find(seedDto.CategoryId);
            var user = _context.Users.Find(seedDto.UserId);

            if (category == null || user == null)
            {
                return BadRequest("Invalid CategoryId or UserId");
            }
            if (seedDto.Name.Length < 3)
            {
                return BadRequest("Name must be at least 3 characters long");
            }
            if (seedDto.Description.Length < 50)
            {
                return BadRequest("Description must be at least 50 characters long");
            }

            var seed = new Seed
            {
                Name = seedDto.Name,
                Category = category,
                Description = seedDto.Description,
                UserId = user.Id,
                Stock = seedDto.Stock,
                Date_added = DateTime.UtcNow,
                Price = seedDto.Price,
                Is_active = seedDto.Is_active,
                Image = seedDto.Image
            };

            _context.Seeds.Add(seed);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetOneProduct), new { productId = seed.Id }, seed);
        }

        /// <summary>
        /// Update a product. ADMIN ONLY
        /// </summary>
        /// <param name="Id">Product to update</param>
        /// <param name="seedDto"></param>
        /// <returns></returns>
        [HttpPut]
        [RequireAuth(Permission.PRODUCT_MODIFY)]
        public IActionResult UpdateProduct(int Id, [FromBody] SeedUpdateDTO? seedDto)
        {
            if (seedDto == null || Id == 0)
                return BadRequest("Invalid Id or SeedDto");
            var seed = _context.Seeds.Find(Id);
            if (seed == null)
                return BadRequest("Seed not found");
            if (seedDto.Name != null)
                seed.Name = seedDto.Name;
            if (seedDto.Description != null)
                seed.Description = seedDto.Description;
            if (seedDto.Stock != null)
                seed.Stock = (int)seedDto.Stock;
            if (seedDto.Price != null)
                seed.Price = (int)seedDto.Price;
            if (seedDto.Is_active != null)
                seed.Is_active = (bool)seedDto.Is_active;
            if (seedDto.Image != null)
                seed.Image = seedDto.Image;
            _context.SaveChanges();
            return Ok(seed);
        }

        /// <summary>
        /// Get a list of products.
        /// </summary>
        /// <remarks>
        /// You can get a list of products by providing a page number and page size.
        /// You can also filter the products by providing a CategoryId (OPTIONAL).
        /// Page number should be incremented by 1 on the frontend. Otherwise the user will not see the products that come after pagesize.
        /// </remarks>
        /// <param name="Page">Page number</param>
        /// <param name="PageSize">Number of products should the page have</param>
        /// <param name="CategoryId">CategoryId of the products you want to get</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetProduct(int Page, int PageSize, int? CategoryId)
        {
            if (Page < 1)
            {
                return BadRequest("Page number cannot be smaller than 1");
            }
            if (PageSize > 100)
            {
                return BadRequest("Page Size cannot be bigger than 100");
            }

            if (CategoryId != null)
            {
                var products = _context
                    .Seeds
                    .Include(s => s.Category)
                    .Where(x => x.CategoryId == CategoryId)
                    .Skip((Page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
                return Ok(products);
            }
            else
            {
                var products = _context
                    .Seeds
                    .Include(s => s.Category)
                    .Skip((Page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
                return Ok(products);
            }
        }

        /// <summary>
        /// Get a single product.
        /// </summary>
        /// <remarks>
        /// You can get a single product by providing a productId.
        /// you can also get a list of products by providing a categoryId.
        /// </remarks>
        /// <param name="productId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("find")]
        public IActionResult GetOneProduct(int? productId, int? categoryId)
        {
            if ((productId == null) == (categoryId == null))
                return BadRequest(
                    "Why the fuck are you filling (or leaving them empty) the both integers at the same time?"
                );

            if (productId != null)
            {
                Seed? seed = _context.Seeds.Where(x => x.Id == productId).FirstOrDefault();
                if (seed == null)
                    return BadRequest("Seed not found");
                return Ok(seed);
            }

            if (categoryId != null)
            {
                Seed[] seeds = [.. _context.Seeds.Where(x => x.CategoryId == categoryId)];
                return Ok(seeds);
            }

            return BadRequest("This shouldn't have happend...");
        }
    }
}
