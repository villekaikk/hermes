namespace Hermes.Domain.Models;

public record RequestOptions(
        RequestMethodOption Method,
        string RequestUrl,
        IReadOnlyCollection<Header> Headers,
        IReadOnlyCollection<Parameter> Parameters
    );