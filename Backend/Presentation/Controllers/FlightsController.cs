using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

public class FlightsController : BaseController
{
    private readonly IFlightsService _flightsService;

    public FlightsController(ILogger<BaseController> logger, IConfiguration configuration,
        IFlightsService flightsService) : base(logger,
        configuration)
    {
        _flightsService = flightsService;
    }

    [HttpPost("search-flight")]
    public async Task<IActionResult> SearchFlight([FromBody] FlightSearchRequestDto? dto)
    {
        if (dto is null)
            return BadRequest();

        return Ok(await _flightsService.SearchFlight(dto));
    }
}