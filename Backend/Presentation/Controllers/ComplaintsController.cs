using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplaintsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public ComplaintsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitComplaint([FromBody] ComplaintDto complaint)
        {
            string emailContent = $"Dear {complaint.LastName} {complaint.FirstName},\n\n" +
                         "Your complaint has been submitted successfully!\n\n" +
                         "Thank you for reaching out to us.\n\n" +
                         "Best regards,\n" +
                         "AirLegance Team";

            // Send email using the SendGrid service
            await _emailService.SendEmail(complaint.Email, "Complaint Submission", emailContent);

            // Optionally, you can return a response indicating the success of the operation
            return Ok();
        }
    }

 
}
