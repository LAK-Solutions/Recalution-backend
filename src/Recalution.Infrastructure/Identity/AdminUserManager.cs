using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recalution.Application.Dtos;
using Recalution.Application.Interfaces;
using Recalution.Application.Results;

namespace Recalution.Infrastructure.Identity;

public class AdminUserManager(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
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

    public Task<UserDetailsDto?> GetByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return false;

        var result = await userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public Task<RoleChangeResult> AddUserRolesAsync(
        Guid adminUserId,
        Guid userId,
        IReadOnlyList<string> roles)
    {
        Func<List<string>, IList<string>, List<string>> selectRolesToAdd =
            (validRoles, currentRoles) =>
                validRoles
                    .Where(r => !currentRoles.Contains(r, StringComparer.OrdinalIgnoreCase))
                    .ToList();

        return ChangeUserRolesAsync(
            adminUserId,
            userId,
            roles,
            selectRolesToAdd,
            userManager.AddToRolesAsync
        );
    }

    public Task<RoleChangeResult> RemoveUserRolesAsync(
        Guid adminUserId,
        Guid userId,
        IReadOnlyList<string> roles)
    {
        Func<List<string>, IList<string>, List<string>> selectRolesToRemove =
            (validRoles, currentRoles) =>
                validRoles
                    .Where(r => currentRoles.Contains(r, StringComparer.OrdinalIgnoreCase))
                    .ToList();

        return ChangeUserRolesAsync(
            adminUserId,
            userId,
            roles,
            selectRolesToRemove,
            userManager.RemoveFromRolesAsync
        );
    }

    private async Task<RoleChangeResult> ChangeUserRolesAsync(
        Guid adminUserId,
        Guid userId,
        IReadOnlyList<string> roles,
        Func<List<string>, IList<string>, List<string>> selectRoles,
        Func<AppUser, IEnumerable<string>, Task<IdentityResult>> applyRoles
    )
    {
        if (adminUserId == userId)
            return new RoleChangeResult(false, Array.Empty<string>());

        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return new RoleChangeResult(false, Array.Empty<string>());

        var validRoles = roles
            .Where(r => IdentityDataSeeder.AllowedRoles.Contains(r))
            .ToList();

        if (validRoles.Count == 0)
            return new RoleChangeResult(true, Array.Empty<string>());

        var currentRoles = await userManager.GetRolesAsync(user);

        var selectedRoles = selectRoles(validRoles, currentRoles);

        if (selectedRoles.Count == 0)
            return new RoleChangeResult(true, Array.Empty<string>());

        var result = await applyRoles(user, selectedRoles);

        return result.Succeeded
            ? new RoleChangeResult(true, selectedRoles)
            : new RoleChangeResult(false, Array.Empty<string>());
    }
}