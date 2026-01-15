using Microsoft.AspNetCore.Identity;
using Recalution.Application.Dtos;
using Recalution.Application.Interfaces;
using Recalution.Application.Results;

namespace Recalution.Application.Admin;

public sealed class AdminService(IAdminUserManager manager)
{
    public Task<IReadOnlyList<UserDetailsDto>> GetAllUsersAsync()
        => manager.GetAllUsersAsync();

    public Task<bool> DeleteUserAsync(string userId)
        => manager.DeleteUserAsync(userId);

    public Task<RoleChangeResult> AddUserRolesAsync(string adminUserId, string userId, IReadOnlyList<string> roles)
        => manager.AddUserRolesAsync(adminUserId, userId, roles);

    public Task<RoleChangeResult> RemoveUserRolesAsync(string adminUserId, string userId, IReadOnlyList<string> roles) =>
        manager.RemoveUserRolesAsync(adminUserId, userId, roles);
}