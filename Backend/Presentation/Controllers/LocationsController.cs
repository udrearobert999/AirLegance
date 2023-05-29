using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

public class LocationsController : BaseController
{
    private readonly ILocationsService _locationsService;

    public LocationsController(ILogger<BaseController> logger, IConfiguration configuration,
        ILocationsService locationsService) : base(logger,
        configuration)
    {
        _locationsService = locationsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLocations()
    {
        return Ok(await _locationsService.GetLocations());
    }
}