using Hermes.Domain.Models;
using ReactiveUI;

namespace Hermes.Application.ViewModels.Models;

public partial class RequestListOptionViewModel : ReactiveObject
{
    public delegate void KeyChangedEventHandler();
    
    private event KeyChangedEventHandler KeyChanged;
    
    public string Key
    {
        get => _key;
        set
        {
            this.RaiseAndSetIfChanged(ref _key, value);
            KeyChanged?.Invoke();
        }
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

    public RequestListOption Item { get; }

    public RequestListOptionViewModel(RequestListOption listOption, KeyChangedEventHandler eventHandler)
    {
        Key = listOption.Key;
        Value = listOption.Value;
        Active = listOption.Active;
        Item = listOption;
        KeyChanged += eventHandler;
    }

    private string _key = string.Empty!;
    private string _value  = string.Empty!;
    private bool _active;
}