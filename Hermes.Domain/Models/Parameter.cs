namespace Hermes.Domain.Models;

public record Parameter(string Key, string Value, bool Active = true) : ListOption(Key, Value, Active)
{
    public static Parameter Empty => new (string.Empty, string.Empty);
}