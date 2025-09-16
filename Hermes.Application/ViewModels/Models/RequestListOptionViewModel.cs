using Hermes.Domain.Models;
using ReactiveUI;

namespace Hermes.Application.ViewModels.Models;

public partial class RequestListOptionViewModel : ReactiveObject
{
    public delegate void ListObjectUpdatedEventHandler();
    
    public event ListObjectUpdatedEventHandler KeyChanged;
    public event ListObjectUpdatedEventHandler ValueChanged;
    public event ListObjectUpdatedEventHandler ActiveChanged;
    
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
        set
        {
            this.RaiseAndSetIfChanged(ref _value, value);
            ValueChanged?.Invoke();
        }
    }

    public bool Active
    {
        get => _active;
        set
        {
            this.RaiseAndSetIfChanged(ref _active, value);
            ActiveChanged?.Invoke();
        }
    }

    public RequestListOption Item { get; }

    public RequestListOptionViewModel(RequestListOption listOption)
    {
        Key = listOption.Key;
        Value = listOption.Value;
        Active = listOption.Active;
        Item = listOption;
    }

    private string _key = string.Empty!;
    private string _value  = string.Empty!;
    private bool _active;
}