using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Security;
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

        /// <summary>
        /// Send a contact form
        /// </summary>
        /// <remarks>
        /// This endpoint requires captcha. To see how to send captcha, please refer to the documentation.
        /// Contact form should include Name, Email and Message fields. None of these fields can be empty or null.
        /// Email field should contain a valid email address and the message length should be at least 50 characters.
        /// </remarks>
        /// <param name="formDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Captcha]
        public IActionResult SendContact([FromBody] ContactForm formDetails) //REQUIRES AUTHENTICATION
        {
            if (formDetails == null)
            {
                return BadRequest("Invalid form data.");
            }
            if (
                string.IsNullOrEmpty(formDetails.Name)
                || string.IsNullOrEmpty(formDetails.Email)
                || string.IsNullOrEmpty(formDetails.Message)
            )
            {
                return BadRequest("Invalid form data.");
            }
            if (!formDetails.Email.Contains("@") || !formDetails.Email.Contains("."))
            {
                return BadRequest("Invalid email address.");
            }
            if (formDetails.Message.Length < 10)
            {
                return BadRequest("Message is too short.");
            }
            MailSender
                .SendMail(
                    "sehirbahceleri@gmail.com",
                    "New sehirbahceleri.com.tr Contact Form from: " + formDetails.Name,
                    "USER MAIL: " + formDetails.Email + ", \n\n" + formDetails.Message,
                    "sehirbahceleri.com.tr"
                )
                .Wait();
            return Ok("Message sent!");
        }
    }
}
