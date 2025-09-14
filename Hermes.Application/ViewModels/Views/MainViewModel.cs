using System.Reactive;
using Hermes.Domain.Models;
using ReactiveUI;

namespace Hermes.Application.ViewModels.Views;

public class MainViewModel : ReactiveObject
{
    
    private int _selectedIndex = 0;
    private string _requestUrl = string.Empty;
    private RequestMethodOption _selectedMethod = null!;
    private SendRequestCallback? _sendRequestCallback;

    public ReactiveCommand<Unit, Unit> SendRequestCommand { get; }

    public delegate Task SendRequestCallback(CancellationToken cancellationToken);
    
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

    public void RegisterSendRequestCallback(SendRequestCallback sendRequestCallback) =>
        _sendRequestCallback = sendRequestCallback ?? throw new ArgumentNullException(nameof(sendRequestCallback));

    public MainViewModel()
    {
        SendRequestCommand = ReactiveCommand.CreateFromTask(SendRequestAsync);
        SendRequestCommand.ThrownExceptions.Subscribe(ex => Console.WriteLine($"Exception thrown: {ex}"));
    }

    private async Task SendRequestAsync(CancellationToken token)
    {
        if (_sendRequestCallback != null)
            await _sendRequestCallback(token);
    }
}
