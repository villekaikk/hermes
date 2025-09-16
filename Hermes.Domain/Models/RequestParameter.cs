namespace Hermes.Domain.Models;

public record RequestParameter(string Key, string Value, bool Active) : ListOption(Key, Value, Active)
{
    public static RequestParameter Empty => new (string.Empty, string.Empty, true);
}