using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
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
        
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var apiKey = _emailOptions.SendGridKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("sdtbsp10@gmail.com", "Test Demo User");
            var to = new EmailAddress(email,"Last User");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", message);
            var response = await client.SendEmailAsync(msg);
        }
        // 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  'ConfirmationEmail'
        /*
        private Task Execute(string SendGridApiKey, string subject, string message, string email)
        {
            //var apiKey = Environment.GetEnvironmentVariable("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");
            var client = new SendGridClient(SendGridApiKey);
            var from = new EmailAddress("sdtbsp10@gmail.com", "Main Music Store");
            var to = new EmailAddress(email,"Last User");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, " " ,message);
            return client.SendEmailAsync(msg);
        }
        */
        /*
        static async Task Execute(string email, string subject, string message)
        {
            var apiKey = _configuration["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@authdemo.com", "JWT Demo User");
            var subject = subject;
            var to = new EmailAddress(email);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, message, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
        */

    }
}
