using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Hermes.Application.ViewModels;

namespace Hermes.UI;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}