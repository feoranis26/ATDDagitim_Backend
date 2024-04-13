using ATDBackend.Database.DBContexts;
using ATDBackend.Database.Models;
using ATDBackend.Security.SessionSystem;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ATDBackend.Utils;

namespace ATDBackend.Security
{
    public class RequireMASTER() : ActionFilterAttribute
    {
        private static IActionResult UnauthorizedResponse(object? value = null)
        {
            return new ObjectResult(value)
            {
                StatusCode = 401
            };
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string? masterSecret = Environment.GetEnvironmentVariable("MASTER_SECRET");
            string? masterPw = Environment.GetEnvironmentVariable("MASTER_PW");

            if(masterSecret.IsNullOrEmpty() || masterPw.IsNullOrEmpty())
            {
                context.Result = UnauthorizedResponse();
                return;
            }



            string? signatureStr = context.HttpContext.Request.Headers["signature"];

            if (signatureStr.IsNullOrEmpty())
            {
                context.Result = UnauthorizedResponse();
                return;
            }

            Signature? signature = DecryptSignature(signatureStr, masterSecret);

            if(signature == null || signature.Value.Password != masterPw)
            {
                context.Result = UnauthorizedResponse();
                return;
            }


            DateTime min = DateTime.Now - TimeSpan.FromSeconds(5);
            DateTime max = DateTime.Now + TimeSpan.FromSeconds(5);
            DateTime signTime = signature.Value.SignTime;

            if(signTime > max || signTime < min)
            {
                context.Result = UnauthorizedResponse();
                return;
            }

            await next();

        }




        public struct Signature
        {
            public DateTime SignTime;
            public string Password;
        }

        public static Signature? DecryptSignature(string signatureString, string secret)
        {
            try
            {
                Signature signature;

                string iv = signatureString.Substring(0, 8);
                byte[] encrypted = Convert.FromBase64String(signatureString.Substring(8, signatureString.Length - 8));

                string decrypted = Encoding.UTF8.GetString(Encryption.TripleDES.Decrypt(encrypted, iv, secret));

                if (decrypted.Substring(0, 8) != iv) return null;

                string[] pairs = decrypted.Substring(8, decrypted.Length - 8).Split('/');

                signature.Password = pairs[1];

                string datetimeStr = pairs[0];

                int[] datetimePairs = datetimeStr.Split('$').Select(x => int.Parse(x)).ToArray();

                int sec = datetimePairs[0];
                int minute = datetimePairs[1];
                int hour = datetimePairs[2];
                int day = datetimePairs[3];
                int month = datetimePairs[4];
                int year = datetimePairs[5];

                signature.SignTime = new DateTime(year, month, day, hour, minute, sec);

                return signature;
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
