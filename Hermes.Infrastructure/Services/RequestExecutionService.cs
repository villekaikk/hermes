using Hermes.Application.Interfaces;
using Hermes.Domain.Models;

namespace Hermes.Infrastructure.Services;

public class RequestExecutionService : IRequestExecutionService
{
    public async Task ExecuteRequestAsync(RequestOptions options, CancellationToken token)
    {
        Console.WriteLine($"Sending {options.Method.Value} request to {options.RequestUrl}...");
        await Task.Delay(200, token);
        Console.WriteLine("Request sent");
    }
}