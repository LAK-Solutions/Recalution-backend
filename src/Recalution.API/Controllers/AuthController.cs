using Microsoft.AspNetCore.Mvc;
using Recalution.Application.DTO.Auth;
using Recalution.Application.Interfaces;

namespace Recalution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        await authService.RegisterAsync(dto);
        return Ok("User created successfully!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await authService.LoginAsync(dto);
        return Ok(new { token });
    }
}