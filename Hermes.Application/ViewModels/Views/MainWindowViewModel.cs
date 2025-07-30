using ReactiveUI;

namespace Hermes.Application.ViewModels.Views;

public class MainWindowViewModel(MainViewModel mainViewModel, RequestInfoViewModel requestInfoViewModel, ResponseInfoViewModel responseInfoViewModel) : ReactiveObject
{
    private MainViewModel _mainViewModel = mainViewModel;
    private RequestInfoViewModel _requestInfoViewModel = requestInfoViewModel;
    private ResponseInfoViewModel _responseInfoViewModel = responseInfoViewModel;
    private string _version = "v0.0.1a"; 

    public MainWindowViewModel() : this(new MainViewModel(), new RequestInfoViewModel(), new ResponseInfoViewModel()) { }

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

    public ResponseInfoViewModel ResponseInfoViewModel
    {
        get => _responseInfoViewModel;
        set => this.RaiseAndSetIfChanged(ref _responseInfoViewModel, value);
    }

    public string Version
    {
        get => _version;
        set => this.RaiseAndSetIfChanged(ref _version, value);
    }
}