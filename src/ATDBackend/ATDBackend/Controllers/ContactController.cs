using ATDBackend.Database.DBContexts; //DB Contexts
using ATDBackend.Modules;
using ATDBackend.Security;
using Microsoft.AspNetCore.Mvc;

namespace ATDBackend.Controllers
{
    public class ContactForm()
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
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
            if (formDetails == null
                || string.IsNullOrEmpty(formDetails.Email)
                || string.IsNullOrEmpty(formDetails.Message)
            ) return BadRequest("invalidform");

            if (!PatternVerifier.VerifyEmail(formDetails.Email)) return BadRequest("invalidemail");
            if (formDetails.Message.Length < 10) return BadRequest("messageshort");

            if (formDetails.Name == "") formDetails.Name = null;

            bool suc = MailModule.SendMail(
                "sehirbahceleri+contact@gmail.com", $"Contact Form ({formDetails.Name ?? "No Name"})",
                $"Email: {formDetails.Email}\n" +
                $"Name: {formDetails.Name}\n" +
                $"Message: {formDetails.Message}"
                );

            return suc ? Ok("OK") : StatusCode(500, "fail");
        }
    }
}
