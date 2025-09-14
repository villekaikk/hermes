using System.Collections.ObjectModel;
using Hermes.Application.ViewModels.Models;
using Hermes.Domain.Models;
using ReactiveUI;

namespace Hermes.Application.ViewModels.Views;

public class RequestInfoViewModel : ReactiveObject
{
    private ObservableCollection<RequestListOptionViewModel> _parameters = [];
    private ObservableCollection<RequestListOptionViewModel> _headers  = [];

    public ObservableCollection<RequestListOptionViewModel> Parameters
    {
        get => _parameters;
        set => this.RaiseAndSetIfChanged(ref _parameters, value);
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

    private void UpdateParamList()
    {
        if (!string.IsNullOrEmpty(Parameters.Last().Key))
        {
            Parameters.Add(new RequestListOptionViewModel(RequestParameterExtensions.EmptyParameter, UpdateParamList));
        }
    }
    
    private void UpdateHeaderList()
    {
        if (!string.IsNullOrEmpty(Headers.Last().Key))
        {
            Headers.Add(new RequestListOptionViewModel(RequestHeaderExtensions.EmptyHeader, UpdateHeaderList));
        }
    }

    public RequestInfoViewModel()
    {
        Parameters = [new RequestListOptionViewModel(RequestParameterExtensions.EmptyParameter, UpdateParamList)];
        Headers = [new RequestListOptionViewModel(RequestHeaderExtensions.EmptyHeader, UpdateHeaderList)];
    }
}