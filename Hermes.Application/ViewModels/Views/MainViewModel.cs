using System.Reactive;
using Hermes.Application.Utils;
using Hermes.Domain.Models;
using ReactiveUI;

namespace Hermes.Application.ViewModels.Views;

public class MainViewModel : ReactiveObject
{
    private int _selectedIndex;
    private string _requestUrl = string.Empty;
    private string _queryString = string.Empty;
    private RequestMethodOption _selectedMethod = null!;
    private SendRequestCallback? _sendRequestCallback;

    public ReactiveCommand<Unit, Unit> SendRequestCommand { get; }
    
    public delegate void QueryParamUpdatedEventHandler(List<QueryParam> queryParams);
    public event  QueryParamUpdatedEventHandler QueryParamUpdated;

    public delegate Task SendRequestCallback(CancellationToken cancellationToken);
    
    public string RequestUrl
    {
        get => _requestUrl;
        set
        {
            this.RaiseAndSetIfChanged(ref _requestUrl, value);
            if(_requestUrl.Contains('?') && !string.IsNullOrWhiteSpace(_requestUrl.Split('?')[1]))
                UpdateQueryString(_requestUrl.Split('?')[1]);
        }
    }

    private void UpdateQueryString(string requestUrl)
    {
        var queryParams = requestUrl.ParseQueryParams();
        QueryParamUpdated?.Invoke(queryParams);
    }

    public int SelectedIndex
    {
        get => _selectedIndex;
        set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
    }

    public IReadOnlyList<RequestMethodOption> Methods => [
        RequestMethodOption.Get,
        RequestMethodOption.Post,
        RequestMethodOption.Put,
        RequestMethodOption.Patch,
        RequestMethodOption.Delete,
        RequestMethodOption.Options
    ];

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

    public void UpdateQueryString()
    {
        
    }

    private async Task SendRequestAsync(CancellationToken token)
    {
        if (string.IsNullOrEmpty(RequestUrl)) return;
        
        if (_sendRequestCallback != null)
            await _sendRequestCallback(token);
    }
}
