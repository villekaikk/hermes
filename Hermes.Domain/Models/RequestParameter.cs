namespace Hermes.Domain.Models;

public class RequestParameter(string key, string value, bool enabled) : RequestListOption(key, value, enabled) { }
