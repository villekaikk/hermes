namespace Hermes.Domain.Models;

public record Header(string Key, string Value) : ListOption(Key, Value)
{
    public static Header Empty => new (string.Empty, string.Empty);
}