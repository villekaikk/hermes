using System.Collections.ObjectModel;
using Hermes.Application.Interfaces;
using Hermes.Application.Utils;
using Hermes.Application.ViewModels.Models;
using Hermes.Domain.Models;
using ReactiveUI;

namespace Hermes.Application.ViewModels.Views;

public class RequestInfoViewModel : ReactiveObject
{
    private ObservableCollection<ListOptionViewModel> _parameters = [];
    private ObservableCollection<ListOptionViewModel> _headers  = [];
    private readonly List<ListOptionViewModel> _defaultHeaders = [];
    private readonly IQueryParamChannel? _channel;
    private bool _eventHandlingOngoing;

    public ObservableCollection<ListOptionViewModel> Parameters
    {
        get => _parameters;
        set
        {
            this.RaiseAndSetIfChanged(ref _parameters, value);
            if (!_eventHandlingOngoing)
                _channel?.NotifyQueryParamsUpdated(
                    _parameters
                        .Select(p => new Parameter(p.Key, p.Value))
                        .ToList());
        }
    }
    
    public IReadOnlyCollection<Parameter> ParameterList
        => Parameters
            .Where(p => p.IsActive && !string.IsNullOrEmpty(p.Key) && !string.IsNullOrEmpty(p.Value))
            .Select(p => p.Item as Parameter)
            .ToList()
            .AsReadOnly()!;
    
    public IReadOnlyCollection<Header> HeaderList
        => Headers
            .Where(h => h.IsActive && !string.IsNullOrEmpty(h.Key) && !string.IsNullOrEmpty(h.Value))
            .Select(h => h.Item as Header).ToList().AsReadOnly()!;

    public ObservableCollection<ListOptionViewModel> Headers
    {
        get => _headers;
        set => this.RaiseAndSetIfChanged(ref _headers, value);
    }

    private void QueryStringUpdatedEventHandler(List<Parameter> queryParams)
    {
        try
        {
            _eventHandlingOngoing = true;   // Don't delegate these changes back to MainViewModel
            Parameters.Clear();
            if (queryParams.Count > 0)
            {
                queryParams.ForEach(p => AddParam(new Parameter(p.Key, p.Value)));
            }

            EnsureEmptyParam();
        }
        finally
        {
            _eventHandlingOngoing = false;
        }
    }

    private void EnsureEmptyParam()
    {
        if (Parameters.Count > 0 || string.IsNullOrEmpty(Parameters.Last().Key))
            return;
        
        AddParam(Parameter.Empty);
    }
    
    private void EnsureEmptyHeader()
    {
        if (Headers.Count > 0 && string.IsNullOrEmpty(Headers.Last(h => !h.IsDefault).Key))
            return;
        
        AddHeader(Header.Empty);
    }

    private void ParameterChanged()
    {
        var queryParams = Parameters
            .Where(p => p.IsActive)
            .Select(p => new Parameter(p.Key, p.Value))
            .ToList();
        _channel?.NotifyQueryParamsUpdated(queryParams);
    }

    private void AddParam(Parameter param, bool isActive = true, bool isDefault = false)
    {
        var newParam = new ListOptionViewModel(param, isActive, isDefault);
        newParam.KeyChanged += EnsureEmptyParam;
        newParam.KeyChanged += ParameterChanged;
        newParam.ValueChanged += ParameterChanged;
        newParam.ActiveChanged += ParameterChanged;
        Parameters.Add(newParam);
    }

    private void AddHeader(Header header)
    {
        var realCount = Headers.Count(h => !h.IsDefault);
        var newHeader = new ListOptionViewModel(header);
        newHeader.KeyChanged += EnsureEmptyHeader;
        Headers.RemoveAllFrom(realCount);   
        Headers.Add(newHeader);

        if (realCount < 1) 
            return;

        AddDefaultHeaders();
    }
    
    private void AddDefaultHeaders()
    {
        Headers.Add(new ListOptionViewModel(new Header("User-Agent", "HermesClient"), true, true));
        Headers.Add(new ListOptionViewModel(new Header("Accept", "*/*"), true, true));
    }

    public RequestInfoViewModel() { }
    public RequestInfoViewModel(IQueryParamChannel chl)
    {
        _channel = chl;
        _channel.QueryStringUpdated += QueryStringUpdatedEventHandler;
        AddParam(Parameter.Empty);
        AddHeader(Header.Empty);
        AddDefaultHeaders();
    }
}