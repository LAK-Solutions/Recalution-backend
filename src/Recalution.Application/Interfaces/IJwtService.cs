using Recalution.Application.DTO.Jwt;

namespace Recalution.Application.Interfaces;

public interface IJwtService
{
    string GenerateJwt(JwtUserDto user);
}