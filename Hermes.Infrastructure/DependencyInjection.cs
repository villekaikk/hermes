using Hermes.Application.Interfaces;
using Hermes.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hermes.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IRequestExecutionService, RequestExecutionService>();
    
        return services;
    }
}