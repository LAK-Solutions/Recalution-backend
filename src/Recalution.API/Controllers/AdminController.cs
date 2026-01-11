using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recalution.Infrastructure.Identity;

namespace Recalution.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userManager.Users
            .Select(u => new
            {
                u.Id,
                u.Email,
            })
            .ToListAsync();

        var usersWithRoles = new List<object>();
        foreach (var u in users)
        {
            var roles = await _userManager.GetRolesAsync(
                await _userManager.FindByIdAsync(u.Id)
            );

            usersWithRoles.Add(new
            {
                u.Id,
                u.Email,
                Roles = roles
            });
        }

        return Ok(usersWithRoles);
    }

    [HttpPut("users/{userId}/role")]
    public async Task<IActionResult> ChangeUserRole(
        string userId,
        [FromBody] string role)
    {
        // Prevent admin from changing their own role
        var currentAdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentAdminId == userId)
            return BadRequest("Admin cannot change their own role");

        // Find the target user
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound("User not found");

        // Validate the requested role exists
        if (!await _roleManager.RoleExistsAsync(role))
            return BadRequest("Role does not exist");

        // Remove all current roles
        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);

        // Add the new role
        await _userManager.AddToRoleAsync(user, role);

        // Return confirmation
        return Ok(new
        {
            userId = user.Id,
            newRole = role
        });
    }
}