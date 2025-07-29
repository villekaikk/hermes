namespace Hermes.Domain.Models;

public class RequestHeader(string key, string value,  bool enabled) : RequestListOption(key, value, enabled) { }