using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace ATDBackend.Controllers
{
    public class ContactForm()
    {
        public string Name { get; set; }
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
        public IActionResult SendContact([FromBody] ContactForm formDetails) //REQUIRES AUTHENTICATION
        {
            MailSender
                .SendMail(
                    "sehirbahceleri@gmail.com",
                    "New sehirbahceleri.com.tr Contact Form from: " + formDetails.Name,
                    "USER MAIL: " + formDetails.Email + ", \n\n" + formDetails.Message,
                    "sehirbahceleri.com.tr",
                    senderMail: formDetails.Email
                )
                .Wait();
            return Ok("Message sent!");
        }
    }
}
