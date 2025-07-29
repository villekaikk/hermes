using System.Reactive;
using Hermes.Application.Interfaces;
using Hermes.Domain.Models;
using ReactiveUI;

namespace Hermes.Application.ViewModels.Views;

public class MainViewModel : ReactiveObject
{
    
    private int _selectedIndex = 0;
    private string _requestUrl = string.Empty;
    private RequestMethodOption _selectedMethod = null!;
    private readonly IRequestExecutionService _requestExecutor = null!;

    public ReactiveCommand<Unit, Unit> SendRequestCommand { get; } = null!;

    public string RequestUrl
    {
        get => _requestUrl;
        set => this.RaiseAndSetIfChanged(ref _requestUrl, value);
    }

    public int SelectedIndex
    {
        get => _selectedIndex;
        set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
    }

    public IReadOnlyList<RequestMethodOption> Methods => RequestMethodOption.MethodOptionsList;

    public RequestMethodOption SelectedMethod
    {
        get => _selectedMethod;
        set => this.RaiseAndSetIfChanged(ref _selectedMethod, value);
    }

    public MainViewModel() { }

    public MainViewModel(IRequestExecutionService? requestExecutionService)
    {
        SendRequestCommand = ReactiveCommand.CreateFromTask(SendRequest);
        SendRequestCommand.ThrownExceptions.Subscribe(ex => Console.WriteLine($"Exception thrown: {ex}"));

        _requestExecutor = requestExecutionService ?? throw new ArgumentNullException(nameof(requestExecutionService));
    }

    private async Task SendRequest(CancellationToken token)
    {
        var options = new RequestOptions(SelectedMethod, RequestUrl);
        await _requestExecutor.ExecuteRequestAsync(options, token)!;
    }
}
