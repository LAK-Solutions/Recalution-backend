using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recalution.Application.Admin;
using Recalution.Infrastructure.Identity;

namespace Recalution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly AdminService _adminService;

    public AdminController(AdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _adminService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPut("users/{userId}/roles")]
    public async Task<IActionResult> AddUserRoles(
        string userId,
        [FromBody] IReadOnlyList<string> roles)
    {
        var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var currentAdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var ok = await _adminService.AddUserRolesAsync(currentAdminId, userId, roles);

        if (!ok) return BadRequest("Role change failed");
        return Ok(new { userId, roles });
    }
}