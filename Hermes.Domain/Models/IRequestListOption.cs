namespace Hermes.Domain.Models;

public abstract class RequestListOption(string name, string value, bool enabled)
{
    public string Name { get; set; } = name;
    public string Value { get; set; } = value;
    public bool Enabled { get; set; } = enabled;
}