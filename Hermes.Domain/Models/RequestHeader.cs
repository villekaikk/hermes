namespace Hermes.Domain.Models;

public record RequestHeader(string Key, string Value, bool Active) : RequestListOption(Key, Value, Active)
{
    public static RequestHeader Empty =>  new RequestHeader(string.Empty, string.Empty, true);
}