namespace Hermes.Domain.Models;

public abstract class RequestListOption(string key, string value, bool enabled)
{
    public string Key { get; set; } = key;
    public string Value { get; set; } = value;
    public bool Enabled { get; set; } = enabled;
}