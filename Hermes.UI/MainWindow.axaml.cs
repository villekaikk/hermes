using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Hermes.Application.ViewModels;

namespace Hermes.UI;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}