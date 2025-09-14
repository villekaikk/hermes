namespace Hermes.Domain.Models;

public record RequestHeader(string Key, string Value,  bool Active) : RequestListOption(Key, Value, Active) { }