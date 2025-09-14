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
        => Parameters.Select(p => p.Item as RequestParameter).ToList().AsReadOnly()!;
    
    public IReadOnlyCollection<RequestHeader> HeaderList
        => Headers.Select(h => h.Item as RequestHeader).ToList().AsReadOnly()!;

    public ObservableCollection<RequestListOptionViewModel> Headers
    {
        get => _headers;
        set => this.RaiseAndSetIfChanged(ref _headers, value);
    }

    public RequestInfoViewModel()
    {
        Parameters = [
            new RequestListOptionViewModel(new RequestParameter("param1", "value1", true)),
            new RequestListOptionViewModel(new RequestParameter("param2", "value2", false))
        ];
        Headers = [
            new RequestListOptionViewModel(new RequestHeader("header1", "value1", false)),
            new RequestListOptionViewModel(new RequestHeader("header2", "value2", true))
        ];

    }
}