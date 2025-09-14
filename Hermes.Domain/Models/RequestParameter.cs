namespace Hermes.Domain.Models;

public record RequestParameter(string Key, string Value, bool Active) : RequestListOption(Key, Value, Active) { }
