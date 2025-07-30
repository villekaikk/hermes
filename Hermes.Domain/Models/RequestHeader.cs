namespace Hermes.Domain.Models;

public class RequestHeader(string key, string value,  bool active) : RequestListOption(key, value, active) { }