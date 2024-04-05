using System.Text;
using System.Web;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;

namespace ATDBackend.Security
{
    public class Captcha : ActionFilterAttribute
    {
        private enum CaptchaResult
        {
            ERROR = 0,
            VALID,
            INVALID
        }

        private async Task<CaptchaResult> VerifyCaptcha(string cliResp)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(5);
                string? CaptchaSecret = Environment.GetEnvironmentVariable("CAPTCHA_SECRET");
                if (CaptchaSecret == null)
                    return CaptchaResult.ERROR;

                // object obj = new { secret = CaptchaSecret, response = cliResp };
                // var content = JsonContent.Create(obj);
                // var stringContent = await content.ReadAsStringAsync();
                // byte[] byteArrayContent = Encoding.ASCII.GetBytes(stringContent);
                // var newContent = HttpUtility.UrlEncode(byteArrayContent);

                var data = new Dictionary<string, string>
                {
                    { "secret", CaptchaSecret },
                    { "response", cliResp }
                };

                var newerContent = new FormUrlEncodedContent(data);

                Console.WriteLine("What JSON req body looks like: " + data);
                var response = await client.PostAsync(
                    "https://www.google.com/recaptcha/api/siteverify",
                    newerContent
                );
                Console.WriteLine("This is response: " + response);
                dynamic json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("This is response json: " + json);

                bool suc = json.success;

                Console.WriteLine("This is suc who ccalls a variable suc?: " + suc);

                client.Dispose();

                return suc ? CaptchaResult.VALID : CaptchaResult.INVALID;
            }
            catch (Exception)
            {
                return CaptchaResult.ERROR;
            }
        }

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            string? cliresp = context.HttpContext.Request.Query["captcha"];
            Console.WriteLine("CAPTCHING RN");
            Console.WriteLine(cliresp);

            if (cliresp == null)
            {
                context.HttpContext.Response.StatusCode = 400;
                await context.HttpContext.Response.WriteAsync("IncorrectCaptcha");
                Console.WriteLine("Null Captcha");
                return;
            }

            CaptchaResult result = await VerifyCaptcha(cliresp);
            Console.WriteLine("THIS IS RESULT: " + result);
            Console.WriteLine("This is valid captcha should be like: " + CaptchaResult.VALID);
            if (result == CaptchaResult.VALID)
            {
                await next();
                Console.WriteLine("Next");
            }
            else
            {
                context.HttpContext.Response.StatusCode = 400;
                await context.HttpContext.Response.WriteAsync("IncorrectCaptcha");
                Console.WriteLine("actually Incorrect Captcha ");
                return;
            }
        }
    }
}
