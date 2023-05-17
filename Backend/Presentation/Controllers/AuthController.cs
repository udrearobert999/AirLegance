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
        var authResponse = await _authService.AuthenticateUserAsync(loginRequestDto);

        if (!authResponse.Response.Succeeded)
        {
            return Unauthorized(authResponse.Response);
        }

        if (authResponse.AccessToken is null || authResponse.RefreshToken is null)
            return Unauthorized();

        Response.Cookies.Append("X-Access-Token", authResponse.AccessToken, new CookieOptions
        {
            HttpOnly = true
        });

        Response.Cookies.Append("X-Refresh-Token", authResponse.RefreshToken, new CookieOptions
        {
            HttpOnly = true
        });

        return Ok(authResponse.Response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["X-Refresh-Token"];
        if (refreshToken == null)
        {
            return Unauthorized();
        }

        var authResponse = await _authService.RefreshTokenAsync(refreshToken);

        if (!authResponse.Response.Succeeded)
        {
            return Unauthorized(authResponse.Response);
        }

        if (authResponse.AccessToken is null || authResponse.RefreshToken is null)
            return Unauthorized();

        Response.Cookies.Append("X-Access-Token", authResponse.AccessToken, new CookieOptions
        {
            HttpOnly = true
        });

        Response.Cookies.Append("X-Refresh-Token", authResponse.RefreshToken, new CookieOptions
        {
            HttpOnly = true
        });

        return Ok(authResponse.Response);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(string refreshToken)
    {
        var result = await _authService.InvalidateTokenAsync(refreshToken);

        if (!result)
        {
            return Unauthorized();
        }

        return NoContent();
    }

    [Authorize]
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok();
    }
}