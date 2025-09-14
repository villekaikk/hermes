using Hermes.Domain.Models;
using ReactiveUI;

namespace Hermes.Application.ViewModels.Models;

public partial class RequestListOptionViewModel : ReactiveObject
{
    public string Key
    {
        get => _key;
        set => this.RaiseAndSetIfChanged(ref _key, value);
    }

    public string Value
    {
        get => _value;
        set => this.RaiseAndSetIfChanged(ref _value, value);
    }

    public bool Active
    {
        get => _active;
        set => this.RaiseAndSetIfChanged(ref _active, value);
    }

    public RequestListOption Item => _item;
    
    public RequestListOptionViewModel() {}

    public RequestListOptionViewModel(RequestListOption listOption)
    {
        Key = listOption.Key;
        Value = listOption.Value;
        Active = listOption.Active;
        _item = listOption;
    }

    public RequestListOption GetListOption() => _item;

    private string _key = string.Empty!;
    private string _value  = string.Empty!;
    private bool _active;
    private readonly RequestListOption _item;
}