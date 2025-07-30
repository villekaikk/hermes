using Avalonia.ReactiveUI;
using Hermes.Application.ViewModels.Views;
using ReactiveUI;

namespace Hermes.UI;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        this.WhenActivated(disposables => { });
        InitializeComponent();
    }
}