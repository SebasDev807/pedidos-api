namespace DeliveryApi.Controllers;

using DeliveryApi.DTOs.Auth;
using DeliveryApi.Exceptions;
using DeliveryApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            var response = await _authService.RegisterAsync(registerDto);
            return Created("", response);
        }
        catch (ConflictExcption err)
        {
            return Conflict(new { message = err.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        try
        {
            var response = await _authService.LoginAsync(loginRequestDto);
            return Ok(response);
        }
        catch (Exception err)
        {
            return Unauthorized(new {message = err.Message});
        }
    }
}