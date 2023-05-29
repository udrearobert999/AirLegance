using Application.Interfaces;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Infrastructure.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly string _sendGridApiKey;

        public SendGridEmailService()
        {
            // TODO: it is generaly a good practice to move it in appsetings.json
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
