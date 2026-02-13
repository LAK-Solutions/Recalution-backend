using Microsoft.Extensions.DependencyInjection;
using Recalution.Application.Interfaces.Repositories;
using Recalution.Application.Interfaces.Services;
using Recalution.Application.Services;

namespace Recalution.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDeckService, DeckService>();
        services.AddScoped<IFlashCardService, FlashCardService>();

        return services;
    }
}