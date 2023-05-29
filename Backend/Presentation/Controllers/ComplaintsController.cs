using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    public class ComplaintsController : BaseController
    {
        private readonly IEmailService _emailService;

        public ComplaintsController(ILogger<BaseController> logger, IConfiguration configuration,
            IEmailService emailService) : base(logger, configuration)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitComplaint([FromBody] ComplaintDto complaint)
        {
            await _emailService.SendEmail(complaint);

            return Ok();
        }
    }
}