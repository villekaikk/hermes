namespace Hermes.Domain.Models;

public record Request(
    RequestMethodOption Method,
    string RequestUrl,
    IReadOnlyCollection<Header> Headers,
    IReadOnlyCollection<Parameter> Parameters)
{
    public string ToUri() =>
        new UriBuilder(RequestUrl)
        {
            Query = string.Join("&", Parameters.Select(x => $"{x.Key}={x.Value}"))
        }.Uri.AbsoluteUri;
}