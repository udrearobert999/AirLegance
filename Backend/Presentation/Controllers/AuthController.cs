using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers;

public class AuthController : BaseController
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(ILogger<BaseController> logger,
        IConfiguration configuration,
        IUserService userService,
        IAuthService authService) :
        base(logger, configuration)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(UserRegistrationRequestDto registrationRequestDto)
    {
        var responseData = await _userService.CreateUserAsync(registrationRequestDto);
        if (!responseData.Succeeded)
        {
            return BadRequest(responseData);
        }

        return Ok(responseData);
    }

    [HttpPost("login")]
    public async Task<IActionResult> AuthenticateUserAsync(UserLoginRequestDto loginRequestDto)
    {
        var jwtResponseDto = await _authService.AuthenticateUserAsync(loginRequestDto);

        if (!jwtResponseDto.Succeeded)
        {
            return BadRequest(jwtResponseDto);
        }

        if (jwtResponseDto.Data is null)
        {
            return BadRequest();
        }

        Response.Cookies.Append("jwt", jwtResponseDto.Data.Jwt, new CookieOptions
        {
            HttpOnly = true
        });

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("test")]
    public IActionResult Test()
    {
        return Ok();
    }
}