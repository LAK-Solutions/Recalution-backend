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
        var currentAdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentAdminId is null)
            return Unauthorized();

        var result = await _adminService.AddUserRolesAsync(currentAdminId, userId, roles);

        if (!result.Success) return BadRequest("Role change failed");

        return Ok(new
        {
            userId,
            addedRoles = result.ChangedRoles
        });
    }

    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var ok = await _adminService.DeleteUserAsync(userId);

        if (!ok)
            return NotFound("User not found");

        return NoContent();
    }

    [HttpDelete("users/{userId}/roles")]
    public async Task<IActionResult> RemoveUserRoles(
        string userId,
        [FromBody] IReadOnlyList<string> roles)
    {
        var currentAdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentAdminId is null)
            return Unauthorized();

        var result = await _adminService.RemoveUserRolesAsync(currentAdminId, userId, roles);

        if (!result.Success)
            return BadRequest("Role removal failed");

        return Ok(new
        {
            userId,
            removedRoles = result.ChangedRoles
        });
    }
}