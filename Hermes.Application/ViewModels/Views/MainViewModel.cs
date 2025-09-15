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
        get => $"{_requestUrl}{(_queryString.Length > 0 ? "?" + QueryString : string.Empty )}";
        set
        {
            if (value.Contains('?') && value.Split('?').Length > 1)
            {
                var splits = value.Split('?');
                _requestUrl = splits[0];
                QueryString = splits[1];
            }
            else
            {
                this.RaiseAndSetIfChanged(ref _requestUrl, value);
            }
        }
    }

    public string QueryString
    {
        get => _queryString;
        set
        {
            this.RaiseAndSetIfChanged(ref _queryString, value);
            if (!string.IsNullOrWhiteSpace(_queryString) && _queryString.TryParseQueryParams(out var queryParams))
            {
                Console.WriteLine($"QueryParams: {queryParams.Select(p => $"{p.Key}={p.Value}").Aggregate((a, b) => $"{a}, {b}")}");
                //QueryParamUpdated?.Invoke(queryParams);
            }
        }
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
