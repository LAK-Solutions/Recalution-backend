namespace Recalution.Application.DTO.Jwt;

public record JwtUserDto(
    string Id,
    string? Email,
    IReadOnlyList<string> Roles
);