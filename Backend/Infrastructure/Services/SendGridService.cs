using Application.Interfaces;
using SendGrid.Helpers.Mail;
using SendGrid;
using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SendGridService : IEmailService
    {
        private readonly string _sendGridApiKey;

        public SendGridService()
        {
            _sendGridApiKey = "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";
        }

        public async Task SendEmail(string recipientEmail, string subject, string body)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress("airlegance@gmail.com", "AirLegance");
            var to = new EmailAddress(recipientEmail);
            var email = MailHelper.CreateSingleEmail(from, to, subject, body, body);

            await client.SendEmailAsync(email);
        }
    }
}
