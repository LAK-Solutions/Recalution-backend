using Microsoft.AspNetCore.Identity;
using Recalution.Application.Dtos;
using Recalution.Application.Interfaces;

namespace Recalution.Application.Admin;

public sealed class AdminService(IAdminUserManager manager)
{
    public Task<IReadOnlyList<UserDetailsDto>> GetAllUsersAsync()
        => manager.GetAllUsersAsync();
    
    public Task<bool> DeleteUserAsync(string userId)
        => manager.DeleteUserAsync(userId);

    public Task<bool> AddUserRolesAsync(string adminUserId, string userId, IReadOnlyList<string> roles)
        => manager.AddUserRolesAsync(adminUserId, userId, roles);
}