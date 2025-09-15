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

    public void QueryStringUpdatedEventHandler(List<QueryParam> queryParams)
    {
        Parameters.Clear();
        // queryParams.ForEach(p => Console.WriteLine($"{p.Key}: {p.Value}"));
        if (queryParams.Count > 0)
        {
            queryParams.ForEach(p => 
                Parameters.Add(new RequestListOptionViewModel(new RequestParameter(p.Key, p.Value, true) , EnsureEmptyParam))
                );
        }
        EnsureEmptyParam();
    }

    private void EnsureEmptyParam()
    {
        if (Parameters.Count == 0 || !string.IsNullOrEmpty(Parameters.Last().Key))
        {
            Parameters.Add(new RequestListOptionViewModel(RequestParameter.Empty, EnsureEmptyParam));
        }
    }
    
    private void EnsureEmptyHeader()
    {
        if (Headers.Count == 0 || !string.IsNullOrEmpty(Headers.Last().Key))
        {
            Headers.Add(new RequestListOptionViewModel(RequestHeader.Empty, EnsureEmptyHeader));
        }
    }

    public RequestInfoViewModel()
    {
        Parameters = [new RequestListOptionViewModel(RequestParameter.Empty, EnsureEmptyParam)];
        Headers = [new RequestListOptionViewModel(RequestHeader.Empty, EnsureEmptyHeader)];
    }
}