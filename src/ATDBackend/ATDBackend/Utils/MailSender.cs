using System;
using BCrypt.Net;
using Mailjet.Client;
using Mailjet.Client.Resources;
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
        /// <exception cref="MailException">If sending the mail fails, this exception is thrown with the error message and status code.</exception>
        /// <returns>Returns an async task with a mailjet response.</returns>
        public static async Task<MailjetResponse> SendMail(
            string email,
            string subject,
            string body,
            string userName,
            string? bodyHtml = null,
            string? senderMail = "sehirbahceleri@gmail.com"
        )
        {
            String actualHtml = "<p>" + body + "</p>";
            if (bodyHtml != null)
            {
                actualHtml = bodyHtml;
            }
            MailjetClient client = new MailjetClient(
                Environment.GetEnvironmentVariable("MJ_APIKEY_PUBLIC"),
                Environment.GetEnvironmentVariable("MJ_APIKEY_PRIVATE")
            );
            MailjetRequest request = new MailjetRequest { Resource = Send.Resource, }
                .Property(Send.FromEmail, senderMail)
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
                // Console.WriteLine(
                //     string.Format(
                //         "Total: {0}, Count: {1}\n",
                //         response.GetTotal(),
                //         response.GetCount()
                //     )
                // );
                // Console.WriteLine(response.GetData());
                return response;
            }
            else
            {
                // Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                // Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                // Console.WriteLine(response.GetData());
                // Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
                throw new MailException(response.GetErrorMessage(), response.StatusCode);
            }
        }
    }

    /// <summary>
    /// Custom exception for mail sending errors.
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <param name="errorCode"></param>
    public class MailException(string errorMessage, int errorCode) : Exception
    {
        public string ErrorMessage { get; set; } = errorMessage;
        public int ErrorCode { get; set; } = errorCode;
    }
}
