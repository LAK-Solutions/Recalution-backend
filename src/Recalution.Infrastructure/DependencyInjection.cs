using Microsoft.Extensions.DependencyInjection;
using Recalution.Application.Interfaces;
using Recalution.Infrastructure.Identity;
using Recalution.Infrastructure.Services;

namespace Recalution.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAdminUserManager, AdminUserManager>();
        return services;
    }
}