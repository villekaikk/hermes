namespace Hermes.Domain.Models;

public class RequestHeader(string name, string value,  bool enabled) : RequestListOption(name, value, enabled) { }