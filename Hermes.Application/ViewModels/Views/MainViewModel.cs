using System.Reactive;
using Hermes.Application.Interfaces;
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
    private readonly IQueryParamChannel _channel;
    private bool _eventHandlingOngoing; 

    public ReactiveCommand<Unit, Unit> SendRequestCommand { get; }

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
        _channel.NotifyQueryStringUpdated(queryParams);
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

    private void QueryParamsUpdatedEventHandler(List<QueryParam> queryParams)
    {
        try
        {
            _eventHandlingOngoing = true;
            Console.WriteLine("Got new query params!");
        }
        finally
        {
            _eventHandlingOngoing = false;
        }
    }

    public MainViewModel(IQueryParamChannel chl)
    {
        _channel = chl;
        _channel.QueryParamsUpdated += QueryParamsUpdatedEventHandler;
        SendRequestCommand = ReactiveCommand.CreateFromTask(SendRequestAsync);
        SendRequestCommand.ThrownExceptions.Subscribe(ex => Console.WriteLine($"Exception thrown: {ex}"));
    }

    private async Task SendRequestAsync(CancellationToken token)
    {
        if (string.IsNullOrEmpty(RequestUrl)) return;
        
        if (_sendRequestCallback != null)
            await _sendRequestCallback(token);
    }
}
