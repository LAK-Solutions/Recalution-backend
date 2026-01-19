using Recalution.Application.Results;

namespace Recalution.Application.Interfaces;

public interface IAdminUserWriter
{
    Task<bool> DeleteUserAsync(Guid userId);
    Task<RoleChangeResult> AddUserRolesAsync(Guid adminUserId, Guid userId, IReadOnlyList<string> roles);
    Task<RoleChangeResult> RemoveUserRolesAsync(Guid adminUserId, Guid userId, IReadOnlyList<string> roles);
}