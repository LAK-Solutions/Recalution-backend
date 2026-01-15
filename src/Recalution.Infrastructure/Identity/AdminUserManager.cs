using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recalution.Application.Dtos;
using Recalution.Application.Interfaces;
using Recalution.Application.Results;

namespace Recalution.Infrastructure.Identity;

public class AdminUserManager(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    : IAdminUserManager
{
    public async Task<IReadOnlyList<UserDetailsDto>> GetAllUsersAsync()
    {
        var users = await userManager.Users.ToListAsync();
        var result = new List<UserDetailsDto>(users.Count);

        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            result.Add(new UserDetailsDto(user.Id, user.Email!, roles.ToList()));
        }

        return result;
    }

    public Task<UserDetailsDto?> GetByIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteUserAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return false;

        var result = await userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public async Task<RoleChangeResult> AddUserRolesAsync(string adminUserId, string userId, IReadOnlyList<string> roles)
    {
        // admin cannot change own role
        if (adminUserId == userId)
            return new RoleChangeResult(false, Array.Empty<string>());

        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return new RoleChangeResult(false, Array.Empty<string>());


        var validRoles = roles
            .Where(r => IdentityDataSeeder.AllowedRoles.Contains(r))
            .ToList();

        if (validRoles.Count == 0)
            return new RoleChangeResult(true, Array.Empty<string>());

        var currentRoles = await userManager.GetRolesAsync(user);

        var rolesToAdd = validRoles
            .Where(r => !currentRoles.Contains(r, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (rolesToAdd.Count == 0)
            return new RoleChangeResult(true, Array.Empty<string>());

        var addResult = await userManager.AddToRolesAsync(user, rolesToAdd);
        return addResult.Succeeded
            ? new RoleChangeResult(true, rolesToAdd)
            : new RoleChangeResult(false, Array.Empty<string>());
    }

    public async Task<RoleChangeResult> RemoveUserRolesAsync(string adminUserId, string userId, IReadOnlyList<string> roles)
    {
        // admin cannot change own role
        if (adminUserId == userId)
            return new RoleChangeResult(false, Array.Empty<string>());

        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return new RoleChangeResult(false, Array.Empty<string>());


        var validRoles = roles
            .Where(r => IdentityDataSeeder.AllowedRoles.Contains(r))
            .ToList();

        if (validRoles.Count == 0)
            return new RoleChangeResult(true, Array.Empty<string>());

        var currentRoles = await userManager.GetRolesAsync(user);

        var rolesToRemove = validRoles
            .Where(r => currentRoles.Contains(r, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (rolesToRemove.Count == 0)
            return new RoleChangeResult(true, Array.Empty<string>());

        var removeResult = await userManager.RemoveFromRolesAsync(user, rolesToRemove);
        
        return removeResult.Succeeded
            ? new RoleChangeResult(true, rolesToRemove)
            : new RoleChangeResult(false, Array.Empty<string>());
    }
}