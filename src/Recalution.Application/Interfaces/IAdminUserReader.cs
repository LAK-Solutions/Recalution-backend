using Recalution.Application.Dtos;

namespace Recalution.Application.Interfaces;

public interface IAdminUserReader
{
    Task<IReadOnlyList<UserDetailsDto>> GetAllUsersAsync();
    Task<UserDetailsDto?> GetByIdAsync(string userId);
}

