using App.Shared.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace App.Shared.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendReminderNotificationMessageAsync(string to, string projectTitle, DateTimeOffset reminderDate)
        {
            using var client = GetSmtpClient();
            var senderEmail = _configuration["NetworkCredentials:Email"];
            var websiteUrl = _configuration["WebUrls:BaseUrl"];

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail, "Notifications"),
                Subject = $"Reminder: {projectTitle}",
                Body = GenerateReminderEmailBody(projectTitle, reminderDate, websiteUrl),
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }


        public async Task SendNewContactNotificationAsync(string webUser)
        {
            using var client = GetSmtpClient();
            var senderEmail = _configuration["NetworkCredentials:Email"];
            var toUser = _configuration["NetworkCredentials:ContactReceiver"];
            var adminPanelUrl = _configuration["WebUrls:AdminUrl"];

            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = "Yeni Əlaqə Əlavə Olundu - DevIT Group Xəbərdarlıq",
                Body = GenerateNewContactEmailBody(webUser, adminPanelUrl),
                IsBodyHtml = true
            };

            mailMessage.To.Add(toUser);
            await client.SendMailAsync(mailMessage);
        }

        // Specific methods
        private SmtpClient GetSmtpClient()
        {
            var email = _configuration["NetworkCredentials:Email"];
            var appKey = _configuration["NetworkCredentials:AppKey"];

            return new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Credentials = new NetworkCredential(email, appKey)
            };
        }

        private string GenerateNewContactEmailBody(string webUser, string adminPanelUrl)
        {
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
              <style>
                body {{
                  font-family: 'Arial', sans-serif;
                  background-color: #f9f9f9;
                  margin: 0;
                  padding: 0;
                }}
                .container {{
                  max-width: 600px;
                  margin: auto;
                  padding: 20px;
                  background-color: #ffffff;
                  border-radius: 5px;
                  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }}
                h2 {{
                  color: #333333;
                }}
                p {{
                  color: #666666;
                }}
                a {{
                  text-decoration: none;
                  color: #007BFF;
                }}
              </style>
            </head>
            <body>
              <div class='container'>
                <h2>Yeni əlaqə əlavə olundu!</h2>
                <p><strong>{webUser}</strong> adlı istifadəçi kontakt siyahısına əlavə olundu.</p>
                <p>Daha ətraflı məlumat üçün admin panelə keçid edin:</p>
                <p><a href='{adminPanelUrl}'>Admin Panelə Keçid Et</a></p>
              </div>
            </body>
            </html>";
        }

        private string GenerateReminderEmailBody(string projectTitle, DateTimeOffset reminderDate, string websiteUrl)
        {
            return $@"
            <html>
                <body style='font-family: Arial, sans-serif;'>
                    <h2>Reminder Notification</h2>
                    <p>You have a reminder for the project: <b>{projectTitle}</b>.</p>
                    <p><b>Reminder Date:</b> {reminderDate:dd.MM.yyyy HH:mm}</p>
                    <p>You can check details on the website: <a href='{websiteUrl}'>{websiteUrl}</a></p>
                    <br/>
                    <p style='color:gray;font-size:12px;'>This is an automated message. Please do not reply.</p>
                </body>
            </html>";
        }
    }
}
