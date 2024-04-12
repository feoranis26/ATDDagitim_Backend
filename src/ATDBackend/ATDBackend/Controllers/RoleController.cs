using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ATDBackend.Security.SessionSystem;
using ATDBackend.Security; //You know what this is...

namespace ATDBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;

        [HttpGet("all")]
        [RequireMASTER]
        public IActionResult GetRoles() //REQUIRES AUTHENTICATION
        {
            return Ok(_context.Roles.ToList());
        }


        [HttpPost]
        [RequireMASTER]
        public IActionResult AddRole(string roleName, ulong permissionss)
        {
            try
            {
                Role role = new Role()
                {
                    RoleName = roleName,
                    Permissions = permissionss
                };

                _context.Roles.Add(role);
                _context.SaveChanges();

                return Ok(role);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [RequireMASTER]
        public IActionResult ModifyRole(int ID, string roleName, ulong permissionss)
        {
            try
            {
                Role role = new Role()
                {
                    Id = ID,
                    RoleName = roleName,
                    Permissions = permissionss
                };

                _context.Roles.Update(role);
                _context.SaveChanges();

                return Ok(role);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [RequireMASTER]
        public IActionResult DeleteRole(int roleID)
        {
            try
            {
                Role? role = _context.Roles.Find(roleID);
                if (role == null) return NotFound();

                _context.Roles.Remove(role);

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }
    }
}
