using Application.Dto;
using Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

public class AuthController : BaseController
{
    private readonly IValidator<UserRegistrationDto> _userRegistrationValidator;
    private readonly IUserService _userService;

    public AuthController(ILogger<BaseController> logger, IConfiguration configuration,
        IValidator<UserRegistrationDto> userRegistrationValidator, IUserService userService) :
        base(logger, configuration)
    {
        _userRegistrationValidator = userRegistrationValidator;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegistrationDto registration)
    {
        var validationResult = await _userRegistrationValidator.ValidateAsync(registration);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        return Ok(await _userService.CreateUserAsync(registration));
    }
}