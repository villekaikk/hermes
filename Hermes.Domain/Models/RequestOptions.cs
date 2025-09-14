namespace Hermes.Domain.Models;

public record RequestOptions(
        RequestMethodOption Method,
        string RequestUrl,
        IReadOnlyCollection<RequestListOption> Headers,
        IReadOnlyCollection<RequestListOption> Parameters
    );