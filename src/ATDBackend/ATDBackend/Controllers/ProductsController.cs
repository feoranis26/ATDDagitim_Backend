﻿using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ATDBackend.Security.SessionSystem;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public IActionResult AddProduct([FromForm] NewSeedDTO seedDto) //REQUIRES AUTHENTICATION
        {
            
            var category = _context.Categories.Find(seedDto.CategoryId);

            var school = _context.Schools.Find(seedDto.SchooolId);

            if (category == null || school == null)
            {
                return BadRequest("Invalid CategoryId or SchoolId");
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
                SeedContributors = new List<SeedContributor> { new SeedContributor { School = school } },
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
        /// Add contributor school to a product
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="schoolId">School ID</param>
        /// <returns></returns>
        [HttpPost("contributor")]
        [RequireAuth(Permission.PRODUCT_CONTRIBUTOR_MODIFY)]
        public IActionResult AddContributor(int productId, int schoolId) //REQUIRES AUTHENTICATION
        {
            Seed? seed = _context.Seeds.Include(x => x.SeedContributors).Where(x => x.Id == productId).FirstOrDefault();
            School? school = _context.Schools.Find(schoolId);

            if (seed == null) return NotFound("seednotfound");
            if (school == null) return NotFound("schoolnotfound");
            if (!seed.SeedContributors.Where(x => x.SchoolId == schoolId).IsNullOrEmpty()) return BadRequest("contributorexists");

            seed.SeedContributors.Add(new SeedContributor() { SeedId = productId, SchoolId = schoolId});
            _context.Seeds.Update(seed);
            _context.SaveChanges();

            return Ok("OK");
        }

        /// <summary>
        /// Remove a contributor school from a product
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="schoolId">School ID</param>
        /// <returns></returns>
        [HttpDelete("contributor")]
        [RequireAuth(Permission.PRODUCT_CONTRIBUTOR_MODIFY)]
        public IActionResult RemoveContributor(int productId, int schoolId) //REQUIRES AUTHENTICATION
        {
            Seed? seed = _context.Seeds.Include(x => x.SeedContributors).Where(x => x.Id == productId).FirstOrDefault();
            School? school = _context.Schools.Find(schoolId);

            if (seed == null) return NotFound("seednotfound");
            if (school == null) return NotFound("schoolnotfound");

            SeedContributor? contributor = seed.SeedContributors.Where(x => x.SchoolId == schoolId).FirstOrDefault();
            if (contributor == null) return NotFound("contributornotfound");

            seed.SeedContributors.Remove(contributor);
            _context.Seeds.Update(seed);
            _context.SaveChanges();

            return Ok("OK"); // TODO: Check if save changes successful
        }


        /// <summary>
        /// Update a product. ADMIN ONLY
        /// </summary>
        /// <param name="Id">Product to update</param>
        /// <param name="seedDto"></param>
        /// <returns></returns>
        [HttpPut]
        [RequireAuth(Permission.PRODUCT_MODIFY)]
        public IActionResult UpdateProduct(int Id, [FromForm] SeedUpdateDTO? seedDto)
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
            if (PageSize < 1 || PageSize > 100)
            {
                return BadRequest("Page Size must be between 1 and 100");
            }


            var products = _context
                .Seeds
                .Include(s => s.Category)
                .Include(s => s.SeedContributors).ThenInclude(x => x.School)
                .Where(x => CategoryId == null ? true : x.CategoryId == CategoryId)
                .Select(x => new SeedFetchDTO
                {
                    CategoryName = x.Category.CategoryName,
                    Id = x.Id,
                    ContributorSchoolNames = x.SeedContributors.Select(s => s.School.Name).ToArray(),
                    Description = x.Description,
                    Is_active = x.Is_active,
                    Name = x.Name,
                    Price = x.Price,
                    Stock = x.Stock
                })
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            return Ok(products);


        }

        /// <summary>
        /// Get a single product.
        /// </summary>
        /// <remarks>
        /// You can get a single product by providing a productId.
        /// you can also get a list of products by providing a categoryId.
        /// </remarks>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        public IActionResult GetOneProduct(int productId)
        {
            SeedFetchDTO? seed = _context.Seeds
            .Include(s => s.Category)
            .Include(s => s.SeedContributors).ThenInclude(x => x.School)
            .Where(x => x.Id == productId)
            .Select(x => new SeedFetchDTO
            {
                CategoryName = x.Category.CategoryName,
                Id = x.Id,
                ContributorSchoolNames = x.SeedContributors.Select(s => s.School.Name).ToArray(),
                Description = x.Description,
                Is_active = x.Is_active,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock
            }).FirstOrDefault();

            if (seed == null)
            {
                return NotFound("seednotfound");
            }

            return Ok(seed);
        }

        [HttpGet("{productId}/image")]
        public IActionResult GetProductImage(int productId)
        {
            Seed? seed = _context.Seeds.Find(productId);

            if(seed == null)
            return NotFound("seednotfound");

            return File(seed.Image, "image/jpeg");
        }
    }
}
