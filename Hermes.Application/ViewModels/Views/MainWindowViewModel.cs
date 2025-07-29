using ReactiveUI;

namespace Hermes.Application.ViewModels.Views;

public class MainWindowViewModel(MainViewModel mainViewModel, RequestInfoViewModel requestInfoViewModel) : ReactiveObject
{
    private MainViewModel _mainViewModel = mainViewModel;
    private RequestInfoViewModel _requestInfoViewModel = requestInfoViewModel;

    public MainWindowViewModel() : this(new MainViewModel(), new RequestInfoViewModel()) { }

    public MainViewModel MainViewModel
    {
        get => _mainViewModel;
        set => this.RaiseAndSetIfChanged(ref _mainViewModel, value);
    }

    public RequestInfoViewModel RequestInfoViewModel
    {
        get => _requestInfoViewModel;
        set => this.RaiseAndSetIfChanged(ref _requestInfoViewModel, value);
    }
}