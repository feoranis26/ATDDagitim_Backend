using System.Security.Claims;
using ATDBackend.DTO; //Data Transfer Objects
using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Database.Models; //DB Models
using ATDBackend.Security;
using ATDBackend.Utils;
using Microsoft.AspNetCore.Authorization; //You know what this is...
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ATDBackend.Controllers
{
    public class ContactForm()
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class ContactController(
        ILogger<AuthController> logger,
        IConfiguration configuration,
        AppDBContext context
    ) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly AppDBContext _context = context;

        [HttpPost]
        [CheckAuth]
        public IActionResult SendContact([FromBody] ContactForm formDetails) //REQUIRES AUTHENTICATION
        {
            MailSender
                .SendMail(
                    "sehirbahceleri@gmail.com",
                    "New sehirbahceleri.com.tr Contact Form from: " + formDetails.Name,
                    formDetails.Message,
                    "sehirbahceleri.com.tr"
                )
                .Wait();
            return Ok();
        }
    }
}
