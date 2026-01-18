using Recalution.Application.DTO.Auth;

namespace Recalution.Application.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterDto dto);
    Task<string> LoginAsync(LoginDto dto);
}