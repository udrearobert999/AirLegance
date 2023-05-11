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
    private readonly IUsersService _usersService;
    private readonly IAuthService _authService;

    public AuthController(ILogger<BaseController> logger,
        IConfiguration configuration,
        IUsersService usersService,
        IAuthService authService) :
        base(logger, configuration)
    {
        _usersService = usersService;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(UserRegistrationRequestDto registrationRequestDto)
    {
        var responseData = await _usersService.CreateUserAsync(registrationRequestDto);
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

        if (!jwtResponseDto.Response.Succeeded)
        {
            return BadRequest(jwtResponseDto.Response);
        }

        if (jwtResponseDto.Jwt is null)
            return Unauthorized();

        Response.Cookies.Append("jwt", jwtResponseDto.Jwt, new CookieOptions
        {
            HttpOnly = true
        });

        return Ok(jwtResponseDto.Response);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok();
    }
}