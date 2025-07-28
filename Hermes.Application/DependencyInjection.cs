using Hermes.Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Hermes.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainViewModel>();
        
        return services;
    }
}