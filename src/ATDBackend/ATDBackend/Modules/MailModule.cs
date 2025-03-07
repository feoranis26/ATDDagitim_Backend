﻿using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace ATDBackend.Modules
{
    public static class MailModule
    {
        public static ILogger? logger { get; set; }
        public static IConfiguration? config { get; set; }

        private static SmtpClient? Client;

        private static string? username;

        public static bool Initialize()
        {
            if (config == null || logger == null) return false;

            username = Environment.GetEnvironmentVariable("MAIL_USERNAME");
            string? pw = Environment.GetEnvironmentVariable("MAIL_PASSWORD");

            logger.LogInformation($"Mail Init / {username} / {pw}");

            if(username == null || pw == null) return false;

            Client = new SmtpClient(config.GetValue<string>("Mail:SMTPHost"))
            {
                Port = config.GetValue<int>("Mail:SMTPPort"),
                Credentials = new NetworkCredential(username, pw),
                EnableSsl = true
            };

            return true;
        }

        public static async Task<bool> SendMailAsync(string to, string subject, string body)
        {
            try
            {
                if (username == null || Client == null) return false;

                logger?.LogInformation($"Sending mail / {username} / {to} / {subject} / {body}");

                MailMessage msg = new MailMessage(username, to)
                {
                    From = new MailAddress(username),
                    Subject = subject,
                    Body = body
                };

                await Client.SendMailAsync(msg);

                return true;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, null);
                return false;
            }
        }
        public static bool SendMail(string to, string subject, string body)
        {
            return SendMailAsync(to, subject, body).Result;
        }
    }
}
