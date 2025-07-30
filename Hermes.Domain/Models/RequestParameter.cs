namespace Hermes.Domain.Models;

public class RequestParameter(string key, string value, bool active) : RequestListOption(key, value, active) { }
