namespace Hermes.Domain.Models;

public class RequestParameter(string name, string value, bool enabled) : RequestListOption(name, value, enabled) { }
