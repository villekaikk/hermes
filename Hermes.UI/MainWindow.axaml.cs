using Avalonia.ReactiveUI;
using Hermes.Application.ViewModels;

namespace Hermes.UI;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}