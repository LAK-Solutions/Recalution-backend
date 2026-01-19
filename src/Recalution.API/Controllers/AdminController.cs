using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recalution.Application.Interfaces;
using Recalution.Infrastructure.Identity;

namespace Recalution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminUserManager _adminUserManager;

    public AdminController(IAdminUserManager adminUserManager)
    {
        _adminUserManager = adminUserManager;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _adminUserManager.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPut("users/{userId}/roles")]
    public async Task<IActionResult> AddUserRoles(
        Guid userId,
        [FromBody] IReadOnlyList<string> roles)
    {
        var currentAdminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _adminUserManager.AddUserRolesAsync(currentAdminId, userId, roles);

        if (!result.Success) return BadRequest("Role change failed");

        return Ok(new
        {
            userId,
            addedRoles = result.ChangedRoles
        });
    }

    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var ok = await _adminUserManager.DeleteUserAsync(userId);

        return NoContent();
    }

    [HttpDelete("users/{userId}/roles")]
    public async Task<IActionResult> RemoveUserRoles(
        Guid userId,
        [FromBody] IReadOnlyList<string> roles)
    {
        var currentAdminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _adminUserManager.RemoveUserRolesAsync(currentAdminId, userId, roles);

        if (!result.Success)
            return BadRequest("Role removal failed");

        return Ok(new
        {
            userId,
            removedRoles = result.ChangedRoles
        });
    }
}