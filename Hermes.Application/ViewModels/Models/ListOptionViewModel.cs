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

    public bool IsActive
    {
        get => _isIsActive;
        set
        {
            this.RaiseAndSetIfChanged(ref _isIsActive, value);
            ActiveChanged?.Invoke();
        }
    }
    
    public bool IsDefault
    {
        get => _isDefault;
        private init => this.RaiseAndSetIfChanged(ref _isDefault, value);
    }

    public ListOption Item { get; }

    public ListOptionViewModel(ListOption listOption, bool isIsActive = true, bool isDefault = false)
    {
        Key = listOption.Key;
        Value = listOption.Value;
        IsActive = isIsActive;
        IsDefault = isDefault;
        Item = listOption;
    }

    private string _key = string.Empty!;
    private string _value  = string.Empty!;
    private bool _isIsActive;
    private readonly bool _isDefault;
}