using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MainMusicStore.Utility
{
    public class EmailSender : IEmailSender
    {

        private readonly EmailOptions _emailOptions;

        public EmailSender(IOptions<EmailOptions> options)
        {
            _emailOptions = options.Value;
        }
        
        public Task SendEmailAsync(string email, string subject, string message)
        {
            //Execute(_emailOptions.SendGridApiKey, subject, message, email).Wait();
            
            
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("localhost", 25);

            mail.From = new MailAddress("admin@admin.com");
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = message;
            SmtpServer.Send(mail);
            return Task.CompletedTask;
        }
        /*
        private async Task Execute(string SendGridApiKey, string subject, string message, string email)
        {
            //var apiKey = Environment.GetEnvironmentVariable("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");
            var client = new SendGridClient(SendGridApiKey);
            var from = new EmailAddress("iletisim@sdtbsp.com");
            var to = new EmailAddress(email,"Last User");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, " " ,message);
            var response = await client.SendEmailAsync(msg);
        }*/

    }
}
