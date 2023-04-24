using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly ILogger<BaseController> _logger;
    protected readonly IConfiguration? _configuration;
    private readonly ITestService _testService;

    public BaseController(ILogger<BaseController> logger, IConfiguration configuration, ITestService testService)
    {
        _logger = logger;
        _configuration = configuration;
        _testService = testService;
    }

    [HttpGet("generate-test-entity")]
    public IActionResult TestMethod()
    {
        return Ok(_testService.GenerateTestEntity());
    }

}