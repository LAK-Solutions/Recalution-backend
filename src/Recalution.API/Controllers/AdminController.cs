using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recalution.Application.Interfaces;

namespace Recalution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController(IAdminUserManager adminUserManager) : ControllerBase
{
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await adminUserManager.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPut("users/{userId}/roles")]
    public async Task<IActionResult> AddUserRoles(
        Guid userId,
        [FromBody] IReadOnlyList<string> roles)
    {
        var currentAdminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await adminUserManager.AddUserRolesAsync(currentAdminId, userId, roles);

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
        var ok = await adminUserManager.DeleteUserAsync(userId);

        return NoContent();
    }

    [HttpDelete("users/{userId}/roles")]
    public async Task<IActionResult> RemoveUserRoles(
        Guid userId,
        [FromBody] IReadOnlyList<string> roles)
    {
        var currentAdminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await adminUserManager.RemoveUserRolesAsync(currentAdminId, userId, roles);

        if (!result.Success)
            return BadRequest("Role removal failed");

        return Ok(new
        {
            userId,
            removedRoles = result.ChangedRoles
        });
    }
}