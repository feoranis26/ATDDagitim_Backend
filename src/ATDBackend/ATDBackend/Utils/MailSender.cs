using System;
using BCrypt.Net;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;

namespace ATDBackend.Utils
{
    public class MailSender
    {
        /// <summary>
        /// Sends an email to the given email address.
        /// </summary>
        /// <param name="email">Recipient mail</param>
        /// <param name="subject">Mail subject</param>
        /// <param name="body">Mail body</param>
        /// <param name="bodyHtml">Mail body in HTML format</param>
        /// <param name="userName">Recipient username</param>
        /// <returns></returns>
        public static async Task SendMail(
            string email,
            string subject,
            string body,
            string? bodyHtml,
            string userName
        )
        {
            String actualHtml = "<p>body</p>";
            if (bodyHtml != null)
            {
                actualHtml = bodyHtml;
            }
            MailjetClient client = new MailjetClient(
                Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"),
                Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE")
            );
            MailjetRequest request = new MailjetRequest { Resource = Send.Resource, }
                .Property(Send.FromEmail, "sehirbahceleri@gmail.com")
                .Property(Send.FromName, "Şehirbahçeleri")
                .Property(
                    Send.Recipients,
                    new JArray
                    {
                        new JObject { { "Email", email }, { "Name", userName } }
                    }
                )
                .Property(Send.Subject, subject)
                .Property(Send.TextPart, body)
                .Property(Send.HtmlPart, actualHtml);
            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(
                    string.Format(
                        "Total: {0}, Count: {1}\n",
                        response.GetTotal(),
                        response.GetCount()
                    )
                );
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
                throw new mailException(response.GetErrorMessage(), response.StatusCode);
            }
        }
    }

    public class mailException : Exception
    {
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }

        /// <summary>
        /// Custom exception for mail sending errors.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="errorCode"></param>
        public mailException(string errorMessage, int errorCode)
        {
            this.ErrorMessage = errorMessage;
            this.ErrorCode = errorCode;
        }
    }
}
