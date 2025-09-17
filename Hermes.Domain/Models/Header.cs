namespace Hermes.Domain.Models;

public record Header(string Key, string Value, bool Active) : ListOption(Key, Value, Active)
{
    public static Header Empty => new (string.Empty, string.Empty, true);
}