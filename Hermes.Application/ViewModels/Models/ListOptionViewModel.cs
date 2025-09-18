using Hermes.Domain.Models;
using ReactiveUI;

namespace Hermes.Application.ViewModels.Models;

public partial class ListOptionViewModel : ReactiveObject
{
    public delegate void ListObjectUpdatedEventHandler();
    
    public event ListObjectUpdatedEventHandler? KeyChanged;
    public event ListObjectUpdatedEventHandler? ValueChanged;
    public event ListObjectUpdatedEventHandler? ActiveChanged;
    
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
        get => _isActive;
        set
        {
            this.RaiseAndSetIfChanged(ref _isActive, value);
            ActiveChanged?.Invoke();
        }
    }
    
    public bool Default
    {
        get => _isDefault;
        set => this.RaiseAndSetIfChanged(ref _isDefault, value);
    }

    public ListOption Item { get; }

    public ListOptionViewModel(ListOption listOption, bool isActive = true, bool isDefault = false)
    {
        Key = listOption.Key;
        Value = listOption.Value;
        Active = isActive;
        Default = isDefault;
        Item = listOption;
    }

    private string _key = string.Empty!;
    private string _value  = string.Empty!;
    private bool _isActive;
    private bool _isDefault;
}