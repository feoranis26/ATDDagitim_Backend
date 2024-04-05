using Microsoft.AspNetCore.DataProtection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;

namespace ATDBackend.Security
{
    public static class CaptchaVerifier
    {
        public enum CaptchaResult
        {
            ERROR = 0,
            VALID,
            INVALID
        }


        public static async Task<CaptchaResult> VerifyCaptcha(string cliResp)
        {
            try
            {
                HttpClient client = new HttpClient();
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

                return suc ? CaptchaResult.VALID : CaptchaResult.INVALID;
            }
            catch(Exception)
            {
                return CaptchaResult.ERROR;
            }
        }
    }
}
