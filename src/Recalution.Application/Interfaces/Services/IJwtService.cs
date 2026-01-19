using Recalution.Application.DTO.Jwt;

namespace Recalution.Application.Interfaces.Services;

public interface IJwtService
{
    string GenerateJwt(JwtUserDto user);
}