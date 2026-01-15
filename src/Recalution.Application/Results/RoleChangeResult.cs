namespace Recalution.Application.Results;

public sealed record RoleChangeResult(
    bool Success,
    IReadOnlyList<string> ChangedRoles
);