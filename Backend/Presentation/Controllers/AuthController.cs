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
    public async Task<IActionResult> Register(UserRegistrationDto registrationDto)
    {
        var responseData = await _userService.CreateUserAsync(registrationDto);
        if (!responseData.IsValid)
        {
            return BadRequest(responseData);
        }

        return Ok(responseData);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto loginDto)
    {
        var authUser = await _authService.AuthenticateUser(loginDto);
        if (authUser is null)
            return BadRequest("Invalid credentials");

        var jwt = _authService.CreateJwt(authUser);

        Response.Cookies.Append("jwt", jwt, new CookieOptions
        {
            HttpOnly = true
        });

        return Ok(jwt);
    }

    [Authorize]
    [HttpPost("test")]
    public IActionResult Test()
    {
        return Ok();
    }
}