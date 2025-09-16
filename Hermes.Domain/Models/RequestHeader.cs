namespace Hermes.Domain.Models;

public record RequestHeader(string Key, string Value, bool Active) : ListOption(Key, Value, Active)
{
    public static RequestHeader Empty => new (string.Empty, string.Empty, true);
}