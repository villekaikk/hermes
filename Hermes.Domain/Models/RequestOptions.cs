namespace Hermes.Domain.Models;

public record RequestOptions(
        RequestMethodOption Method,
        string RequestUrl,
        IReadOnlyCollection<ListOption> Headers,
        IReadOnlyCollection<ListOption> Parameters
    );