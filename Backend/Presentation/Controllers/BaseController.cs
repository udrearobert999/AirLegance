using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly ILogger<BaseController> _logger;
    protected readonly IConfiguration _configuration;

    public BaseController(ILogger<BaseController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
}