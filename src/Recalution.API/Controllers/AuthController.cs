using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recalution.Infrastructure.Identity;

namespace Recalution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public AuthController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("User created successfully!");
    }

    [HttpGet("emails")]
    public async Task<IActionResult> GetAllEmails()
    {
        var emails = await _userManager.Users
            .Select(u => u.Email)
            .ToListAsync();
        return Ok(emails);
    }
}

public class RegisterDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}