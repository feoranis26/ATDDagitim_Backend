using Microsoft.AspNetCore.Mvc.Filters;
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
                HttpClient client = new() { Timeout = TimeSpan.FromSeconds(5) };
                string? CaptchaSecret = Environment.GetEnvironmentVariable("CAPTCHA_SECRET");
                if (CaptchaSecret == null)
                    return CaptchaResult.ERROR;

                var data = new Dictionary<string, string>
                {
                    { "secret", CaptchaSecret },
                    { "response", cliResp }
                };

                var newerContent = new FormUrlEncodedContent(data);

                var response = await client.PostAsync(
                    "https://www.google.com/recaptcha/api/siteverify",
                    newerContent
                );
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

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
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
