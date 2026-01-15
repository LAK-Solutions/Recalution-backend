namespace Recalution.Application.Dtos;

public sealed record UserDetailsDto(
    string Id,
    string? Email,
    IReadOnlyList<string> Roles
);