namespace Recalution.Application.DTO.Jwt;

public record JwtUserDto(
    Guid Id,
    string? Email,
    IReadOnlyList<string> Roles
);