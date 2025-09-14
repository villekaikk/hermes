namespace Hermes.Domain.Models;

public record RequestParameter(string Key, string Value, bool Active) : RequestListOption(Key, Value, Active) { }

public static class RequestParameterExtensions
{
    public static RequestParameter EmptyParameter =>  new RequestParameter(string.Empty, string.Empty, true);
}
