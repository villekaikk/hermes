using ReactiveUI;

namespace Hermes.Application.ViewModels;

public class MainWindowViewModel(MainViewModel mainViewModel) : ReactiveObject
{
    private MainViewModel _mainViewModel = mainViewModel;

    public MainViewModel MainViewModel
    {
        get => _mainViewModel;
        set => this.RaiseAndSetIfChanged(ref _mainViewModel, value);
    }
}