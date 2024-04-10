using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ATDBackend.Security.SessionSystem; //You know what this is...

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>200 with all the categories in a json object.</returns>
        [HttpGet("all")]
        public IActionResult GetCategories()
        {
            return Ok(_context.Categories.ToList());
        }

        /// <summary>
        /// Adds a new category to the database. ADMIN ONLY
        /// </summary>
        /// <param name="category">Include this in the request body.</param>
        /// <remarks>
        /// This endpoint requires admin authentication. Also you should provide all parameters of the category object:
        /// CategoryName, Description
        /// </remarks>
        /// <returns>Status code 201 with the created category.</returns>
        [HttpPost]
        [RequireAuth(Permission.PERMISSION_ADMIN)]
        public IActionResult AddCategory([FromBody] Category category)
        {
            var actualCategory = new Category
            {
                CategoryName = category.CategoryName,
                Description = category.Description
            };
            _context.Categories.Add(actualCategory);
            _context.SaveChanges();
            return CreatedAtAction(
                nameof(getCategory),
                new { categoryID = actualCategory.Id },
                actualCategory
            );
        }

        /// <summary>
        /// Get a category by ID
        /// </summary>
        /// <remarks>
        /// Please make sure that the category ID exists before calling this endpoint.
        /// </remarks>
        /// <param name="categoryID">Id of the category</param>
        /// <returns>Status code 200 with the category object.</returns>
        [HttpGet]
        public IActionResult getCategory(int categoryID)
        {
            var category = _context.Categories.Find(categoryID);
            if (category is null)
            {
                return BadRequest("Category not found");
            }
            else
            {
                return Ok(category);
            }
        }
    }
}
