namespace Hermes.Domain.Models;

public record RequestHeader(string Key, string Value,  bool Active) : RequestListOption(Key, Value, Active) { }

public static class RequestHeaderExtensions
{
    public static RequestHeader EmptyHeader =>  new RequestHeader(string.Empty, string.Empty, true);
}
