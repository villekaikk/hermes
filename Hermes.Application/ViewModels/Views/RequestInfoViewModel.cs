using System.Collections.ObjectModel;
using Hermes.Application.Interfaces;
using Hermes.Application.ViewModels.Models;
using Hermes.Domain.Models;
using ReactiveUI;

namespace Hermes.Application.ViewModels.Views;

public class RequestInfoViewModel : ReactiveObject
{
    private ObservableCollection<RequestListOptionViewModel> _parameters = [];
    private ObservableCollection<RequestListOptionViewModel> _headers  = [];
    private readonly IQueryParamChannel _channel;
    private bool _eventHandlingOngoing;

    public ObservableCollection<RequestListOptionViewModel> Parameters
    {
        get => _parameters;
        set
        {
            this.RaiseAndSetIfChanged(ref _parameters, value);
            if (!_eventHandlingOngoing)
                _channel.NotifyQueryParamsUpdated(_parameters.Select(p => new QueryParam(p.Key, p.Value)).ToList());
        }
    }
    
    public IReadOnlyCollection<RequestParameter> ParameterList
        => Parameters
            .Where(p => p.Active)
            .Select(p => p.Item as RequestParameter).ToList().AsReadOnly()!;
    
    public IReadOnlyCollection<RequestHeader> HeaderList
        => Headers
            .Where(h => h.Active)
            .Select(h => h.Item as RequestHeader).ToList().AsReadOnly()!;

    public ObservableCollection<RequestListOptionViewModel> Headers
    {
        get => _headers;
        set => this.RaiseAndSetIfChanged(ref _headers, value);
    }

    private void QueryStringUpdatedEventHandler(List<QueryParam> queryParams)
    {
        try
        {
            _eventHandlingOngoing = true;   // Don't delegate these changes back to MainViewModel
            Parameters.Clear();
            Console.WriteLine("We here");
            if (queryParams.Count > 0)
            {
                queryParams.ForEach(p => AddParam(new RequestParameter(p.Key, p.Value, true)));
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
        if (Parameters.Count == 0 || !string.IsNullOrEmpty(Parameters.Last().Key))
        {
            AddParam(RequestParameter.Empty);
        }
    }
    
    private void EnsureEmptyHeader()
    {
        if (Headers.Count == 0 || !string.IsNullOrEmpty(Headers.Last().Key))
        {
            var newHeader = new RequestListOptionViewModel(RequestHeader.Empty);
            newHeader.KeyChanged += EnsureEmptyHeader;
            Headers.Add(newHeader);
        }
    }

    private void ParameterChanged()
    {
        var queryParams = Parameters
            .Where(p => p.Active)
            .Select(p => new QueryParam(p.Key, p.Value))
            .ToList();
        _channel.NotifyQueryParamsUpdated(queryParams);
    }

    private void AddParam(RequestParameter param)
    {
        var newParam = new RequestListOptionViewModel(param);
        newParam.KeyChanged += EnsureEmptyParam;
        newParam.KeyChanged += ParameterChanged;
        newParam.ValueChanged += ParameterChanged;
        newParam.ActiveChanged += ParameterChanged;
        Parameters.Add(newParam);
    }

    private void AddHeader(RequestHeader header)
    {
        var newHeader = new RequestListOptionViewModel(header);
        newHeader.KeyChanged += EnsureEmptyHeader;
        Headers.Add(newHeader);
    }

    public RequestInfoViewModel(IQueryParamChannel chl)
    {
        _channel = chl;
        _channel.QueryStringUpdated += QueryStringUpdatedEventHandler;
        AddParam(RequestParameter.Empty);
        AddHeader(RequestHeader.Empty);
    }
}