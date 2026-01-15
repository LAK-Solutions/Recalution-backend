namespace Recalution.Application.Interfaces;

public interface IAdminUserWriter
{
    Task<bool> DeleteUserAsync(string userId);
    Task<bool> AddUserRolesAsync(string adminUserId, string userId, IReadOnlyList<string> roles);
}