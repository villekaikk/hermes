using System;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Hermes.Application;
using Hermes.Application.ViewModels.Views;
using Hermes.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReactiveUI;
using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace Hermes.UI;

public partial class App : Avalonia.Application
{
    private IServiceProvider Services => _host.Services;
    private IHost _host = null!;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var hostBuilder = new HostBuilder()
            .ConfigureServices((_, services) =>
            {
                services.UseMicrosoftDependencyResolver();
                var resolver = Locator.CurrentMutable;
                resolver.InitializeSplat();
      
                Locator.CurrentMutable.RegisterConstant<IActivationForViewFetcher>(new AvaloniaActivationForViewFetcher());
                Locator.CurrentMutable.RegisterConstant<IPropertyBindingHook>(new AutoDataTemplateBindingHook());
                RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;
      
                services
                    .AddInfrastructure()
                    .AddApplication()
                    .AddPresentation();
            });
    
        _host = hostBuilder.Build();
        _host.Services.UseMicrosoftDependencyResolver();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Services.GetService<MainWindowViewModel>()
            };

            desktop.MainWindow.Show();
        
            desktop.Exit += async (_, _) =>
            {
                using (_host)
                {
                    await _host.StopAsync();
                }
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}