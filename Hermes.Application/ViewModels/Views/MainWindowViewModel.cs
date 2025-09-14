using Hermes.Application.Interfaces;
using Hermes.Domain.Models;
using ReactiveUI;

namespace Hermes.Application.ViewModels.Views;

public class MainWindowViewModel : ReactiveObject
{
    private MainViewModel _mainViewModel;
    private RequestInfoViewModel _requestInfoViewModel;
    private ResponseInfoViewModel _responseInfoViewModel;
    private readonly IRequestExecutionService? _requestExecutionService = null;
    private string _version = "v0.0.1a";

    public MainWindowViewModel()
    {
        // Design time mock
        _mainViewModel = new MainViewModel();
        _mainViewModel.RegisterSendRequestCallback(async (options, token) =>
        {
            Console.WriteLine($"Sending a mock {options.Method.Value} request...");
            await Task.Delay(200, token);
            Console.WriteLine("Mock request sent");
        });
        
        _requestInfoViewModel = new RequestInfoViewModel();
        _responseInfoViewModel = new ResponseInfoViewModel();
    }

    public MainWindowViewModel(
        MainViewModel mainViewModel, RequestInfoViewModel requestInfoViewModel,
        ResponseInfoViewModel responseInfoViewModel, IRequestExecutionService requestExecutionService)
    {
        _mainViewModel = mainViewModel;
        _mainViewModel.RegisterSendRequestCallback(ExecuteRequest);
        
        _requestInfoViewModel = requestInfoViewModel;
        _responseInfoViewModel = responseInfoViewModel;
        _requestExecutionService = requestExecutionService;
    }

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

    private async Task ExecuteRequest(RequestOptions options, CancellationToken cancellationToken)
    {
        await _requestExecutionService!.ExecuteRequestAsync(options, cancellationToken);
    }
}