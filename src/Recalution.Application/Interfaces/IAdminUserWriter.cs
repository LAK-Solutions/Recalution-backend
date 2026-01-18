using Recalution.Application.Results;

namespace Recalution.Application.Interfaces;

public interface IAdminUserWriter
{
    Task<bool> DeleteUserAsync(string userId);
    Task<RoleChangeResult> AddUserRolesAsync(string adminUserId, string userId, IReadOnlyList<string> roles);
    Task<RoleChangeResult> RemoveUserRolesAsync(string adminUserId, string userId, IReadOnlyList<string> roles);
}