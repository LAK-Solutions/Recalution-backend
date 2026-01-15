using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Recalution.Application.Dtos;
using Recalution.Application.Interfaces;

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

    public Task<bool> DeleteUserAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddUserRolesAsync(string adminUserId, string userId, IReadOnlyList<string> roles)
    {
        // admin cannot change own role
        if (adminUserId == userId)
            return false;

        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
            return false;

        
        var validRoles = roles
            .Where(r => IdentityDataSeeder.AllowedRoles.Contains(r))
            .ToList();
        
        if (validRoles.Count == 0)
            return true; 
        
        var currentRoles = await userManager.GetRolesAsync(user);

        var rolesToAdd = validRoles
            .Where(r => !currentRoles.Contains(r, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (rolesToAdd.Count == 0)
            return true;

        var addResult = await userManager.AddToRolesAsync(user, rolesToAdd);
        return addResult.Succeeded;
    }
}