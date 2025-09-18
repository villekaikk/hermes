namespace Hermes.Domain.Models;

public record Parameter(string Key, string Value) : ListOption(Key, Value)
{
    public static Parameter Empty => new (string.Empty, string.Empty);
}