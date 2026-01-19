namespace Recalution.Application.Dtos;

public sealed record UserDetailsDto(
    Guid Id,
    string? Email,
    IReadOnlyList<string> Roles
);