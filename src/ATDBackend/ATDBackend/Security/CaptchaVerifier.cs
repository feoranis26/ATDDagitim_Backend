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
                string? CaptchaSecret = Environment.GetEnvironmentVariable("CAPTCHASECRET");
                if (CaptchaSecret == null) return CaptchaResult.ERROR;

                object obj = new
                {
                    secret = CaptchaSecret,
                    response = cliResp
                };

                var content = JsonContent.Create(obj);
                var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);

                dynamic json = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                bool suc = json.success;

                client.Dispose();

                return suc ? CaptchaResult.VALID : CaptchaResult.INVALID;
            }
            catch (Exception)
            {
                return CaptchaResult.ERROR;
            }
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string? cliresp = context.HttpContext.Request.Query["captcha"];

            if (cliresp == null)
            {

                context.HttpContext.Response.StatusCode = 400;
                await context.HttpContext.Response.WriteAsync("IncorrectCaptcha");
                return;
            }

            CaptchaResult result = await VerifyCaptcha(cliresp);

            if (result == CaptchaResult.VALID)
            {
                await next();
            }
            else
            {
                context.HttpContext.Response.StatusCode = 400;
                await context.HttpContext.Response.WriteAsync("IncorrectCaptcha");
                return;
            }
        }
    }
}
