using Hermes.Application.Interfaces;
using Hermes.Domain.Models;

namespace Hermes.Infrastructure.Services;

public class RequestExecutionService(IHttpClientFactory clientFactory) : IRequestExecutionService
{
    public async Task ExecuteRequestAsync(RequestOptions options, CancellationToken token)
    {
        var method = options.Method.ToString();
        using var client = clientFactory.CreateClient();
        using var message = new HttpRequestMessage();
        Console.WriteLine($"Sending {options.Method} request to {options.RequestUrl} with {options.Headers.Count} headers and {options.Parameters.Count} parameters...");
        await Task.Delay(200, token);
        Console.WriteLine("Request sent");
    }
}