using Hermes.Application.ViewModels.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Hermes.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainViewModel>();
        services.AddSingleton<RequestInfoViewModel>();
        services.AddSingleton<ResponseInfoViewModel>();
        
        return services;
    }
}