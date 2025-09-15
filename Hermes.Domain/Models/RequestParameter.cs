namespace Hermes.Domain.Models;

public record RequestParameter(string Key, string Value, bool Active) : RequestListOption(Key, Value, Active)
{
    public static RequestParameter Empty =>  new RequestParameter(string.Empty, string.Empty, true);
}