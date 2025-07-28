using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;

namespace Hermes.UI;

public static class DependencyInjection
{

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
    
        return services;
    }
}