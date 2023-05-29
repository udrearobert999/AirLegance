using Application.Interfaces;
using SendGrid.Helpers.Mail;
using SendGrid;
using Application.Dto;

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

        public async Task SendEmail(ComplaintDto complaintDto)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var from = new EmailAddress(complaintDto.Email);
            var to = new EmailAddress("airlegance@gmail.com", "AirLegance");
            var sentMail = MailHelper.CreateSingleEmail(from, to, $"Complaint - {complaintDto.Email}",
                complaintDto.Message, complaintDto.Message);

            await client.SendEmailAsync(sentMail);
            var k = sentMail.HtmlContent;

            string acknowledgmentBody = $"Dear {complaintDto.FirstName} {complaintDto.LastName},\n\n" +
                                        "Your complaint has been submitted successfully!\n\n" +
                                        "Thank you for reaching out to us.\n\n" +
                                        "Best regards,\n" +
                                        "AirLegance Team";
            var ackEmail = MailHelper.CreateSingleEmail(to, from, "Complaint Acknowledgment", acknowledgmentBody, acknowledgmentBody);

            await client.SendEmailAsync(ackEmail);
        }
    }
}