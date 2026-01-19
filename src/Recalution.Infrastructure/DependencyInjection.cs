using Microsoft.Extensions.DependencyInjection;
using Recalution.Application.Interfaces;
using Recalution.Application.Interfaces.Services;
using Recalution.Infrastructure.Services;

namespace Recalution.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}