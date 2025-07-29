using Hermes.Domain.Models;
using ReactiveUI;

namespace Hermes.Application.ViewModels.Models;

public partial class RequestListOptionViewModel : ReactiveObject
{
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string Value
    {
        get => _value;
        set => this.RaiseAndSetIfChanged(ref _value, value);
    }

    public bool Enabled
    {
        get => _enabled;
        set => this.RaiseAndSetIfChanged(ref _enabled, value);
    }
    
    public RequestListOptionViewModel() {}

    public RequestListOptionViewModel(RequestListOption listOption)
    {
        Name = listOption.Name;
        Value = listOption.Value;
        Enabled = listOption.Enabled;
        item = listOption;
    }

    public RequestListOption GetListOption() => item;

    private string _name = string.Empty!;
    private string _value  = string.Empty!;
    private bool _enabled  = false!;
    private RequestListOption item = null!;
}