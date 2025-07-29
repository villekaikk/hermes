using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Hermes.Application.ViewModels.Views;

namespace Hermes.UI.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        AvaloniaXamlLoader.Load(this);
    }
}