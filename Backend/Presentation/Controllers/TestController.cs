using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

class TestController : BaseController
{
    private readonly ITestService _testService;

    public TestController(ILogger<BaseController> logger,
        IConfiguration configuration,
        ITestService testService) :
        base(logger, configuration)
    {
        _testService = testService;
    }

    [HttpGet("generate-test-entity")]
    public IActionResult TestMethod()
    {
        return Ok(_testService.GenerateTestEntity());
    }
}